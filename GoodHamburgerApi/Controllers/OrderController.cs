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
        /// - 1 = X Burger = $5,00
        /// - 2 = X Egg = $ 4,50
        /// - 3 = X Bacon = $ 7,00
        /// - 4 = Fries = $ 2,00
        /// - 5 = Soft Drink = $ 2,50
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

        /// <summary>
        /// Update an existing order with a new list of product IDs.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="productIds">Updated list of product IDs</param>
        /// <returns>The updated order</returns>
        /// <remarks>
        /// Sample request body:
        /// [1, 4]
        /// </remarks>
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, List<int> productIds)
        {
            var (updatedOrder, error) = _orderService.UpdateOrder(id, productIds);

            if (error != null)
            {
                return BadRequest(error);
            }

            var response = new
            {
                OrderId = updatedOrder?.Id,
                TotalPrice = updatedOrder?.TotalPrice.ToString("C2", CultureInfo.GetCultureInfo("en-US"))
            };

            return Ok(response);
        }

        /// <summary>
        /// Delete an order based on the id.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Status of deletion</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var success = _orderService.DeleteOrder(id);

            if (!success)
            {
                return NotFound($"Order {id} not found.");
            }

            return NoContent(); // 204 - Successfully deleted
        }
    }
}
