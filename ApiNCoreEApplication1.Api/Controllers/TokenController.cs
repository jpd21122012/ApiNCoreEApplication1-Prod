
using ApiNCoreEApplication1.Api.Helpers;
using ApiNCoreEApplication1.Domain;
using ApiNCoreEApplication1.Domain.Service;
using ApiNCoreEApplication1.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly IService<UserViewModel, User> _userService;

        public TokenController(IConfiguration config, IService<UserViewModel, User> userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<object> Create([FromBody] LoginModel login)
        {
            /*
             * var client = new RestClient("localhost:44341/api/token");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\n    \"Username\": \"jpd21122012\",\n    \"Password\": \"12345\"\n}",  ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
             */
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                var toSerialize = new MessageHelpers<TokenHelper>()
                {
                    Status = 200,
                    Data = new List<TokenHelper> { tokenString }
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
            else
            {
                var tokenString = BuildToken(user);
                var toSerialize = new MessageHelpers<TokenHelper>()
                {
                    Status = 401,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        private TokenHelper BuildToken(UserModel user)
        {

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //attach roles
            foreach (string role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               _config["Jwt:Issuer"],
               _config["Jwt:Issuer"],
               claims,
               notBefore: new DateTimeOffset(DateTime.Now).DateTime,
              expires: DateTime.Now.AddMinutes(60),  //60 min expiry and a client monitor token quality and should request new token with this one expiries
              signingCredentials: creds);

            var accesToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenHelper
            {
                AccessToken = accesToken,
                EmitionDate = token.ValidFrom,
                ExpirationDate = token.ValidTo,
                ExpiresIn = "3600"
            };
        }

        //Authenticates login information, retrieves authorization infomation (roles)
        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

            var userView = _userService.Get(x => x.UserName == login.Username).SingleOrDefault();
            if (userView != null && userView.Password == login.Password)
            {
                user = new UserModel { Name = userView.FirstName + " " + userView.LastName, Email = userView.Email, Roles = new string[] { } };
                foreach (string role in userView.Roles) user.Roles = new List<string>(user.Roles) { role }.ToArray();
                if (userView.IsAdminRole) user.Roles = new List<string>(user.Roles) { "Administrator" }.ToArray();
            }

            return user;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
            public string[] Roles { get; set; }
        }
    }
}