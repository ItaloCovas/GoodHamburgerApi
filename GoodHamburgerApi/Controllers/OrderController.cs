using System.Globalization;
using GoodHamburgerApi.Models;
using GoodHamburgerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     
        ///     [
        ///        1,
        ///        2,
        ///        3
        ///     ]
        ///
        /// </remarks>
        [HttpPost]
        public IActionResult CreateOrder(
            [FromBody, SwaggerParameter(Description = "List of product IDs to create an order. For example, 1 = 'Hamburger', 2 = 'Fries', 3 = 'Soft Drink'.")]
            List<int> productIds)
        {
            var (order, error) = _orderService.CreateOrder(productIds);

            if (error != null)
            {
                return BadRequest(error);
            }

            // i had to treat this inside the controller, due to the types
            var formattedTotalPrice = order?.TotalPrice.ToString("C2", CultureInfo.GetCultureInfo("en-US"));

            
            var response = new
            {
                OrderId = order?.Id,
                TotalPrice = formattedTotalPrice
            };

            return Ok(response);
        }

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

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetAll()?.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound($"Order {id} not found.");

            return Ok(order);
        }
    }
}
