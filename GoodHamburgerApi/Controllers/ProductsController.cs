using GoodHamburgerApi.Models;
using GoodHamburgerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburgerApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Return all products.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// Return all sandwiches.
        /// </summary>
        [HttpGet("sandwiches")]
        public ActionResult<IEnumerable<Product>> GetSandwiches()
        {
            var sandwiches = _productService.GetSandwiches();
            return Ok(sandwiches);
        }

        /// <summary>
        /// Return all extras.
        /// </summary>
        [HttpGet("extras")]
        public ActionResult<IEnumerable<Product>> GetExtras()
        {
            var extras = _productService.GetExtras();
            return Ok(extras);
        }
    }
}
