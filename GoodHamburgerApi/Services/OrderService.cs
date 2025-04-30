using GoodHamburger.Data;
using GoodHamburgerApi.Models;
using GoodHamburgerApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburgerApi.Services
{
    public class OrderService
    {
        private readonly GoodHamburgerContext _context;

        public OrderService(GoodHamburgerContext context)
        {
            _context = context;
        }

        public (Order? Order, string? Error) CreateOrder(List<int> productIds)
        {
            // check same id products like [1,1] and [2,2]
            if (productIds.GroupBy(pid => pid).Any(group => group.Count() > 1))
                return (null, "You can't add the same product more than once.");

            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();

            if (products.Count == 0)
                return (null, "No valid products found.");

            // avoid two identical items
            var groupedByType = products.GroupBy(p => p.Type);
            foreach (var group in groupedByType)
            {
                if (group.Count() > 1)
                    return (null, $"Only one product allowed of type {group.Key}");
            }

            // validate combos and apply discount
            var total = products.Sum(p => p.Price);
            decimal discount = 0;

            bool hasSandwich = products.Any(p => p.Type == ProductType.Sandwich);
            bool hasFries = products.Any(p => p.Type == ProductType.Extra && p.Name == "Fries");
            bool hasSoftDrink = products.Any(p => p.Type == ProductType.Extra && p.Name == "Soft drink");

            if (hasSandwich)
            {
                if (hasFries && hasSoftDrink)
                {
                    discount = 0.20m; 
                }
                else if (hasFries)
                {
                    discount = 0.10m;
                }
                else if (hasSoftDrink)
                {
                    discount = 0.15m; 
                }
            }

            total -= total * discount;

            // saving order
            var newOrder = new Order
            {
                TotalPrice = total
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            // saving order products

            var orderProducts = productIds.Select(pid => new OrderProduct
            {
                OrderId = newOrder.Id,
                ProductId = pid
            }).ToList();

            _context.OrderProducts.AddRange(orderProducts);
            _context.SaveChanges();

            return (newOrder, null);
        }

        public IEnumerable<Order>? GetAll()
        {
            var orders = _context.Orders
                .Include(o => o.OrderProducts)
                .ToList();

            if (orders.Count == 0) return null;

            return orders;
        }
    }
}
