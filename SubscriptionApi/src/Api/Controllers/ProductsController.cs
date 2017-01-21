using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Filters;
using Api.Models;

namespace Api.Controllers
{
    [IdentityBasicAuthentication]
    [Authorize]

    public class ProductsController : ApiController
    {
        //static IList<Product> _products = new List<Product>
        //{ 
        //    new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
        //    new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
        //    new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        //};
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [Authorize]
        public IEnumerable<Product> GetAllProducts()
        {
            return _service.GetAll();
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = _service.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ValidateProductModel]
        public IHttpActionResult Post([FromBody] Product product)
        {            
            var pro = _service.Find(product.Name);
            if (pro != null) return BadRequest("Existing product");
            var id = _service.Add(product).Id;
            return Ok($"new product id: {id}");
        }
    }

}
