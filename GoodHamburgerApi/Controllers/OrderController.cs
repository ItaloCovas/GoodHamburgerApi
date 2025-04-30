using System.Globalization;
using GoodHamburgerApi.Models;
using GoodHamburgerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburgerApi.Controllers
{
    [ApiController]
    [Route("orders")]
    
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create a new order based on a list of product IDs.
        /// </summary>
        /// <param name="productIds">List of product IDs</param>
        /// <returns>A newly created order</returns>
        /// <remarks>
        /// Product IDs:
        /// - 1 = X Burger
        /// - 2 = X Egg
        /// - 3 = X Bacon
        /// - 4 = Fries
        /// - 5 = Soft Drink
        /// 
        /// Sample request body:
        /// [1, 4, 5]
        /// </remarks>
        [HttpPost]
        public IActionResult CreateOrder(List<int> productIds)
        {
            var (order, error) = _orderService.CreateOrder(productIds);

            if (error != null)
            {
                return BadRequest(error);
            }
            
            var response = new
            {
                OrderId = order?.Id,
                TotalPrice = order?.TotalPrice.ToString("C2", CultureInfo.GetCultureInfo("en-US"))
            };

            return Ok(response);
        }

        /// <summary>
        /// Return all orders.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAll();
            if(orders == null)
                return Ok("There are no orders yet.");

            // i had to treat this inside the controller, due to the types
            var formattedOrders = orders.Select(o => new
            {
                OrderId = o.Id,
                TotalPrice = o.TotalPrice.ToString("C2", CultureInfo.GetCultureInfo("en-US"))
            });

            return Ok(formattedOrders);
        }

        /// <summary>
        /// Return a single order based on the id.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetAll()?.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound($"Order {id} not found.");

            var response = new
            {
                OrderId = order?.Id,
                TotalPrice = order?.TotalPrice.ToString("C2", CultureInfo.GetCultureInfo("en-US"))
            };

            return Ok(response);
        }
    }
}
