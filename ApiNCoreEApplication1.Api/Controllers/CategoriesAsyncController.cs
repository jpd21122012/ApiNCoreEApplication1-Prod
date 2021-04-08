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
    /// Categories Async Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesAsyncController : ControllerBase
    {
        private readonly CategoriesServiceAsync<CategoriesViewModel, Category> _categoriesServiceAsync;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoriesServiceAsync"></param>
        public CategoriesAsyncController(CategoriesServiceAsync<CategoriesViewModel, Category> categoriesServiceAsync)
        {
            _categoriesServiceAsync = categoriesServiceAsync;
        }


        /// <summary>
        /// Returns all Categories.
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
                var items = await _categoriesServiceAsync.GetAll();
                if (items.ToList().Count > 0)
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 200,
                        Data = new List<CategoriesViewModel>(items)
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<CategoriesViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }


        // GET api/table/5
        /// <summary>
        /// Returns an specific Categories.
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
                var item = await _categoriesServiceAsync.GetOne(id);
                if (item == null)
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 200,
                        Data = new List<CategoriesViewModel> { item }
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<CategoriesViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        // POST api/table
        /// <summary>
        /// Creates an specific Categories.
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<object> Post([FromBody] CategoriesViewModel user)
        {
            if (user == null)
            {
                var toSerialize = new MessageHelpers<CategoriesViewModel>()
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
                    var id = _categoriesServiceAsync.Add(user);
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 200,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                catch
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 502,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
        }

        /// <summary>
        /// Updates an specific Categories.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutAsync(int id, [FromBody] CategoriesViewModel user)
        {
            if (user == null || user.Id != id)
            {
                var toSerialize = new MessageHelpers<CategoriesViewModel>()
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
                    int retVal = await _categoriesServiceAsync.Update(user);
                    if (retVal == 0 && retVal > 0)
                    {
                        var toSerialize = new MessageHelpers<CategoriesViewModel>()
                        {
                            Status = 200,
                            Data = null
                        };
                        return JsonConvert.SerializeObject(toSerialize);
                    }
                    else
                    {
                        var toSerialize = new MessageHelpers<CategoriesViewModel>()
                        {
                            Status = 404,
                            Data = null
                        };
                        return JsonConvert.SerializeObject(toSerialize);
                    }
                }
                catch
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 502,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
        }

        /// <summary>
        /// Deletes a specific Categories.
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

                int retVal = await _categoriesServiceAsync.Remove(id);
                if (retVal >= 0)
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 200,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<CategoriesViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<CategoriesViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }
    }
}