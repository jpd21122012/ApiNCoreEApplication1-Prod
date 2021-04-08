using ApiNCoreEApplication1.Api.Helpers;
using ApiNCoreEApplication1.Api.Interfaces;
using ApiNCoreEApplication1.Domain;
using ApiNCoreEApplication1.Domain.Service;
using ApiNCoreEApplication1.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace ApiNCoreEApplication1.Api.Controllers
{
    /// <summary>
    /// Products Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase, IDBAbstraction<ProductsViewModel>
    {
        private readonly ProductsService<ProductsViewModel, Product> _productsService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productsService"></param>
        public ProductsController(ProductsService<ProductsViewModel, Product> productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Returns all Products.
        /// </summary>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize]
        [HttpGet]
        public ActionResult<object> Select()
        {
            try
            {

                var test = _productsService.DoNothing();
                var items = _productsService.GetAll();
                if (items.ToList().Count > 0)
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 200,
                        Data = new List<ProductsViewModel>(items)
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<ProductsViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        // GET api/table/5
        /// <summary>
        /// Returns an specific Products.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<object> SelectById(int id)
        {
            try
            {
                var item = _productsService.GetOne(id);
                if (item == null)
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 200,
                        Data = new List<ProductsViewModel> { item }
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<ProductsViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }

        // POST api/table
        /// <summary>
        /// Creates an specific Products.
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<object> Post([FromBody] ProductsViewModel user)
        {
            if (user == null)
            {
                var toSerialize = new MessageHelpers<ProductsViewModel>()
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
                    var id = _productsService.Add(user);
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 200,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                catch
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 502,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
        }

        /// <summary>
        /// Updates an specific Products.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromBody] ProductsViewModel user)
        {
            if (user == null || user.Id != id)
            {
                var toSerialize = new MessageHelpers<ProductsViewModel>()
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
                    int retVal = _productsService.Update(user);
                    if (retVal == 0 && retVal > 0)
                    {
                        var toSerialize = new MessageHelpers<ProductsViewModel>()
                        {
                            Status = 200,
                            Data = null
                        };
                        return JsonConvert.SerializeObject(toSerialize);
                    }
                    else
                    {
                        var toSerialize = new MessageHelpers<ProductsViewModel>()
                        {
                            Status = 404,
                            Data = null
                        };
                        return JsonConvert.SerializeObject(toSerialize);
                    }
                }
                catch
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 502,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
        }

        /// <summary>
        /// Deletes a specific Products.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="401">No Authorized</response>
        /// <response code="404">No Data</response>
        /// <response code="502">Internal Error</response>
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<object> Delete(int id)
        {
            try
            {

                int retVal = _productsService.Remove(id);
                if (retVal >= 0)
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 200,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
                else
                {
                    var toSerialize = new MessageHelpers<ProductsViewModel>()
                    {
                        Status = 404,
                        Data = null
                    };
                    return JsonConvert.SerializeObject(toSerialize);
                }
            }
            catch
            {
                var toSerialize = new MessageHelpers<ProductsViewModel>()
                {
                    Status = 502,
                    Data = null
                };
                return JsonConvert.SerializeObject(toSerialize);
            }
        }
    }
}