using ApiNCoreEApplication1.Api.Helpers;
using ApiNCoreEApplication1.Domain;
using ApiNCoreEApplication1.Domain.Service;
using ApiNCoreEApplication1.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNCoreEApplication1.Api.Controllers
{
    /// <summary>
    /// User Controller Async
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserAsyncController : ControllerBase
    {
        private readonly UserServiceAsync<UserViewModel, User> _userServiceAsync;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userServiceAsync"></param>
        public UserAsyncController(UserServiceAsync<UserViewModel, User> userServiceAsync)
        {
            _userServiceAsync = userServiceAsync;
        }

        /// <summary>
        /// Returns all Users.
        /// </summary>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<object>> SelectAsync()
        {
            try
            {

                var items = await _userServiceAsync.GetAll();
                if (items.ToList().Count > 0)
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 200,
                        Data = new List<UserViewModel>(items)
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<UserViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        /// <summary>
        /// get all active by username
        /// </summary>
        /// <param name="firstname"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetActiveByFirstName/{firstname}")]
        public async Task<IActionResult> GetActiveByFirstNameAsync(string firstname)
        {
            var items = await _userServiceAsync.Get(a => a.IsActive && a.FirstName == firstname);
            return Ok(items);
        }

        // GET api/table/5
        /// <summary>
        /// Returns an specific User.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> SelectByIdAsync(int id)
        {
            try
            {
                var item = await _userServiceAsync.GetOne(id);
                if (item == null)
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 200,
                        Data = new List<UserViewModel> { item }
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<UserViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        // POST api/table
        /// <summary>
        /// Creates an specific Award.
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<object> Post([FromBody] UserViewModel user)
        {
            if (user == null)
            {
                var toSerialize = new MessageHelpers<UserViewModel>()
                {
                    Status = 404,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
            else
            {
                try
                {
                    var id = _userServiceAsync.Add(user);
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 200,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                catch
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 502,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
        }

        /// <summary>
        /// Updates an specific User.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutAsync(int id, [FromBody] UserViewModel user)
        {
            if (user == null || user.Id != id)
            {
                var toSerialize = new MessageHelpers<UserViewModel>()
                {
                    Status = 404,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
            else
            {
                try
                {
                    int retVal = await _userServiceAsync.Update(user);
                    if (retVal == 0 && retVal > 0)
                    {
                        var toSerialize = new MessageHelpers<UserViewModel>()
                        {
                            Status = 200,
                            Data = null
                        };
                        return JsonConvert.SerializeObject(toSerialize);
                    }
                    else
                    {
                        var toSerialize = new MessageHelpers<UserViewModel>()
                        {
                            Status = 404,
                            Data = null
                        };
                        return JsonConvert.SerializeObject(toSerialize);
                    }
                }
                catch
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 502,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
        }

        /// <summary>
        /// Deletes a specific User.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteAsync(int id)
        {
            try
            {

                int retVal = await _userServiceAsync.Remove(id);
                if (retVal >= 0)
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 200,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<UserViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<UserViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }
    }
}