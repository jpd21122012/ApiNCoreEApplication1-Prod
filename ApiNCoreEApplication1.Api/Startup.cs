using ApiNCoreEApplication1.Api.Helpers;
using ApiNCoreEApplication1.Domain.Mapping;
using ApiNCoreEApplication1.Domain.Service;
using ApiNCoreEApplication1.Entity.Context;
using ApiNCoreEApplication1.Entity.UnitofWork;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;


namespace ApiNCoreEApplication1.Api
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Startup
    {

        public static IConfiguration Configuration { get; set; }
        public IWebHostEnvironment HostingEnvironment { get; private set; }
        public static string SwaggerUIPath { get; set; }
        public static string SwaggerUIStyleSheet { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("SwaggerSettings");
            services.Configure<SwaggerSettings>(appSettingsSection);

            SwaggerUIPath = appSettingsSection.Get<SwaggerSettings>().UIPath;
            SwaggerUIStyleSheet = appSettingsSection.Get<SwaggerSettings>().UIStyleSheet;

            Log.Information("Startup::ConfigureServices");

            try
            {
                services.AddControllers(
                opt =>
                {
                    //Custom filters can be added here 
                    //opt.Filters.Add(typeof(CustomFilterAttribute));
                    //opt.Filters.Add(new ProducesAttribute("application/json"));
                }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                #region "API versioning"
                //API versioning service
                services.AddApiVersioning(
                    o =>
                    {
                        //o.Conventions.Controller<UserController>().HasApiVersion(1, 0);
                        o.AssumeDefaultVersionWhenUnspecified = true;
                        o.ReportApiVersions = true;
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        o.ApiVersionReader = new UrlSegmentApiVersionReader();
                    }
                    );

                // format code as "'v'major[.minor][-status]"
                services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    //versioning by url segment
                    options.SubstituteApiVersionInUrl = true;
                });
                #endregion

                //db service
                if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "True")
                    services.AddDbContext<ApiNCoreEApplication1Context>(opt => opt.UseInMemoryDatabase("TestDB-" + Guid.NewGuid().ToString()));
                else
                    services.AddDbContext<ApiNCoreEApplication1Context>(options => options.UseSqlServer(Configuration["ConnectionStrings:ApiNCoreEApplication1DB"]));

                #region "Authentication"
                if (Configuration["Authentication:UseIndentityServer4"] == "False")
                {
                    //JWT API authentication service
                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    }
                     );
                }
                else
                {
                    //Indentity Server 4 API authentication service
                    services.AddAuthorization();
                    //.AddJsonFormatters();
                    services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(option =>
                    {
                        option.Authority = Configuration["Authentication:IndentityServer4IP"];
                        option.RequireHttpsMetadata = false;
                        //option.ApiSecret = "secret";
                        option.ApiName = "ApiNCoreEApplication1";  //This is the resourceAPI that we defined in the Config.cs in the AuthServ project (apiresouces.json and clients.json). They have to be named equal.
                    });

                }
                #endregion

                #region "CORS"
                // include support for CORS
                // More often than not, we will want to specify that our API accepts requests coming from other origins (other domains). When issuing AJAX requests, browsers make preflights to check if a server accepts requests from the domain hosting the web app. If the response for these preflights don't contain at least the Access-Control-Allow-Origin header specifying that accepts requests from the original domain, browsers won't proceed with the real requests (to improve security).
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy-public",
                        builder => builder.AllowAnyOrigin()   //WithOrigins and define a specific origin to be allowed (e.g. https://mydomain.com)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                    //.AllowCredentials()
                    .Build());
                });
                #endregion

                //mvc service
                services.AddMvc(option => option.EnableEndpointRouting = false)
                     .AddNewtonsoftJson();

                #region "DI code"
                //general unitofwork injections
                services.AddTransient<IUnitOfWork, UnitOfWork>();

                //TODO:Add more injections
                //services injections
                services.AddTransient(typeof(AccountService<,>), typeof(AccountService<,>));
                services.AddTransient(typeof(AwardsService<,>), typeof(AwardsService<,>));
                services.AddTransient(typeof(CategoriesService<,>), typeof(CategoriesService<,>));
                services.AddTransient(typeof(EnigmaUsersService<,>), typeof(EnigmaUsersService<,>));
                services.AddTransient(typeof(EnigmaUsersTypeService<,>), typeof(EnigmaUsersTypeService<,>));
                services.AddTransient(typeof(MatchesService<,>), typeof(MatchesService<,>));
                services.AddTransient(typeof(PlayersService<,>), typeof(PlayersService<,>));
                services.AddTransient(typeof(PlayerStatsService<,>), typeof(PlayerStatsService<,>));
                services.AddTransient(typeof(ProductsService<,>), typeof(ProductsService<,>));
                services.AddTransient(typeof(ScoresService<,>), typeof(ScoresService<,>));
                services.AddTransient(typeof(SeasonService<,>), typeof(SeasonService<,>));
                services.AddTransient(typeof(TeamsService<,>), typeof(TeamsService<,>));
                services.AddTransient(typeof(TournamentsService<,>), typeof(TournamentsService<,>));
                services.AddTransient(typeof(UserService<,>), typeof(UserService<,>));

                services.AddTransient(typeof(AccountServiceAsync<,>), typeof(AccountServiceAsync<,>));
                services.AddTransient(typeof(AwardsServiceAsync<,>), typeof(AwardsServiceAsync<,>));
                services.AddTransient(typeof(CategoriesServiceAsync<,>), typeof(CategoriesServiceAsync<,>));
                services.AddTransient(typeof(EnigmaUsersServiceAsync<,>), typeof(EnigmaUsersServiceAsync<,>));
                services.AddTransient(typeof(EnigmaUsersTypeServiceAsync<,>), typeof(EnigmaUsersTypeServiceAsync<,>));
                services.AddTransient(typeof(MatchesServiceAsync<,>), typeof(MatchesServiceAsync<,>));
                services.AddTransient(typeof(PlayersServiceAsync<,>), typeof(PlayersServiceAsync<,>));
                services.AddTransient(typeof(PlayerStatsServiceAsync<,>), typeof(PlayerStatsServiceAsync<,>));
                services.AddTransient(typeof(ProductsServiceAsync<,>), typeof(ProductsServiceAsync<,>));
                services.AddTransient(typeof(ScoresServiceAsync<,>), typeof(ScoresServiceAsync<,>));
                services.AddTransient(typeof(SeasonServiceAsync<,>), typeof(SeasonServiceAsync<,>));
                services.AddTransient(typeof(TeamsServiceAsync<,>), typeof(TeamsServiceAsync<,>));
                services.AddTransient(typeof(TournamentsServiceAsync<,>), typeof(TournamentsServiceAsync<,>));
                services.AddTransient(typeof(UserServiceAsync<,>), typeof(UserServiceAsync<,>));


                //...add other services
                //
                services.AddTransient(typeof(IService<,>), typeof(GenericService<,>));
                services.AddTransient(typeof(IServiceAsync<,>), typeof(GenericServiceAsync<,>));
                #endregion

                //data mapper profiler setting
                Mapper.Initialize((config) =>
                {
                    config.AddProfile<MappingProfile>();
                });

                #region "Swagger API"
                //Swagger API documentation
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "FutFever API",
                        Version = "v1",
                        Description = "This is the Documentation of FutFever API",
                        TermsOfService = new Uri("http://www.enigma-mx.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "Jorge Perales Diaz",
                            Email = "jorgeperalesdiaz@enigma-mx.com",
                            Url = new Uri("http://bit.ly/2VXn312")
                        },
                        License = new OpenApiLicense { Name = "EnigmaMx © 2019" }
                    });
                    c.SwaggerDoc("v2", new OpenApiInfo
                    {
                        Title = "FutFever API",
                        Version = "v2",
                        Description = "This is the Documentation of FutFever API",
                        TermsOfService = new Uri("http://www.enigma-mx.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "Jorge Perales Diaz",
                            Email = "jorgeperalesdiaz@enigma-mx.com",
                            Url = new Uri("http://bit.ly/2VXn312")
                        },
                        License = new OpenApiLicense { Name = "EnigmaMx © 2019" }
                    });
                    //In Test project find attached swagger.auth.pdf file with instructions how to run Swagger authentication 
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Description = "Authorization header using the Bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference{
                                    Id = "Bearer", //The name of the previously defined security scheme.
                                	Type = ReferenceType.SecurityScheme
                                }
                            },new List<string>()
                        }
                    });

                    //c.DocumentFilter<api.infrastructure.filters.SwaggerSecurityRequirementsDocumentFilter>();
                    c.IncludeXmlComments(Directory.GetCurrentDirectory() + "\\" + "ApiNCoreEApplication1.Api.xml");
                });
                #endregion
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            SecurityToken outToken = null;
            Log.Information("Startup::Configure");

            app.UseExceptionHandler(appBuilder =>
            {
                app.Use(async (context, next) =>
                {
                    //If request == /api/*
                    if (context.Request.Path.Value.Split('/')[1] == "api" && context.Request.Path.Value.Split('/')[2] == "info" || context.Request.Path.Value.Split('/')[2] == "token"
                     || context.Request.Path.Value.Split('/')[1] == "swagger")
                    {
                        await next();
                    }
                    else
                    {
                        var bearer = context.Request.Headers.Where(x => x.Key == "Authorization").ToList();
                        if (bearer.Count > 0)
                        {
                            var headerToken = new JwtSecurityTokenHandler().ValidateToken(
                                bearer.First().Value.ToString().Replace("Bearer ", ""),
                                new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = Configuration["Jwt:Issuer"],
                                    ValidAudience = Configuration["Jwt:Issuer"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                                }, out outToken);
                            if (headerToken.IsInRole("Administrator"))
                            {
                                await next();
                            }
                            else
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";

                                _ = context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageHelpers<ConnectionInfo>
                                {
                                    Status = 401,
                                    Data = null
                                }));
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            _ = context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageHelpers<ConnectionInfo>
                            {
                                Status = 401,
                                Data = null
                            }));
                        }
                    }
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        _ = context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageHelpers<ConnectionInfo>
                        {
                            Status = 401,
                            Data = null
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        _ = context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageHelpers<ConnectionInfo>
                        {
                            Status = 500,
                            Data = null
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
            });


            try
            {
                if (env.EnvironmentName == "Development")
                    app.UseDeveloperExceptionPage();
                else
                    app.UseMiddleware<ExceptionHandler>();

                app.UseCors("CorsPolicy-public");  //apply to every request
                app.UseAuthentication(); //needs to be up in the pipeline, before MVC
                app.UseAuthorization();

                app.UseMvc();

                //Swagger API documentation
                app.UseSwagger();


                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiNCoreEApplication1 API V1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "ApiNCoreEApplication1 API V2");
                    c.DisplayOperationId();
                    c.DisplayRequestDuration();
                    //c.InjectStylesheet(SwaggerUIPath, SwaggerUIStyleSheet);
                    c.InjectStylesheet("/Assets/CustomUI.css");
                });

                //migrations and seeds from json files
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "False" && !serviceScope.ServiceProvider.GetService<ApiNCoreEApplication1Context>().AllMigrationsApplied())
                    {
                        if (Configuration["ConnectionStrings:UseMigrationService"] == "True")
                            serviceScope.ServiceProvider.GetService<ApiNCoreEApplication1Context>().Database.Migrate();
                    }
                    //it will seed tables on aservice run from json files if tables empty
                    if (Configuration["ConnectionStrings:UseSeedService"] == "True")
                        serviceScope.ServiceProvider.GetService<ApiNCoreEApplication1Context>().EnsureSeeded();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}


namespace api.infrastructure.filters
{
    public class SwaggerSecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            document.SecurityRequirements = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                },
                new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Basic", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                }
             };

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}