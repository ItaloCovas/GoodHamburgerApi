using GoodHamburgerApi.Models;
using GoodHamburgerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburgerApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("sandwiches")]
        public ActionResult<IEnumerable<Product>> GetSandwiches()
        {
            return Ok(_productService.GetSandwiches());
        }

        [HttpGet("extras")]
        public ActionResult<IEnumerable<Product>> GetExtras()
        {
            return Ok(_productService.GetExtras());
        }
    }
}
