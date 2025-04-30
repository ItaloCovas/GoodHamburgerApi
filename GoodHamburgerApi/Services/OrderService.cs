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
                if (group.Count() > 1 && group.Key.ToString() != "Extra")
                    return (null, $"Only one product allowed of type {group.Key}");
            }

            // validate combos and apply discount
            decimal total = products.Sum(p => p.Price);
            decimal discount = 0;

            bool hasSandwich = products.Count(p => p.Type == ProductType.Sandwich) == 1;
            bool hasFries = products.Count(p => p.Type == ProductType.Extra && p.Name == "Fries") == 1;
            bool hasSoftDrink = products.Count(p => p.Type == ProductType.Extra && p.Name == "Soft Drink") == 1;

            if (hasSandwich)
            {
                if (hasFries && hasSoftDrink) discount = 0.20m;
                else if (hasFries) discount = 0.10m;
                else if (hasSoftDrink) discount = 0.15m;
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

        public (Order? Order, string? Error) UpdateOrder(int id, List<int> productIds)
        {
            var order = _context.Orders.Include(o => o.OrderProducts).FirstOrDefault(o => o.Id == id);
            if (order == null)
                return (null, $"Order {id} not found.");

            // check for repeated product IDs
            if (productIds.GroupBy(pid => pid).Any(g => g.Count() > 1))
                return (null, "You can't add the same product more than once.");

            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
            if (products.Count == 0)
                return (null, "No valid products found.");

            var groupedByType = products.GroupBy(p => p.Type);
            foreach (var group in groupedByType)
            {
                if (group.Count() > 1 && group.Key.ToString() != "Extra")
                    return (null, $"Only one product allowed of type {group.Key}");
            }

            // calculate discount
            decimal total = products.Sum(p => p.Price);
            decimal discount = 0;

            bool hasSandwich = products.Count(p => p.Type == ProductType.Sandwich) == 1;
            bool hasFries = products.Count(p => p.Type == ProductType.Extra && p.Name == "Fries") == 1;
            bool hasSoftDrink = products.Count(p => p.Type == ProductType.Extra && p.Name == "Soft Drink") == 1;

            if (hasSandwich)
            {
                if (hasFries && hasSoftDrink) discount = 0.20m;
                else if (hasFries) discount = 0.10m;
                else if (hasSoftDrink) discount = 0.15m;
            }

            total -= total * discount;


            order.TotalPrice = total;

            var existingProducts = _context.OrderProducts.Where(op => op.OrderId == id);
            _context.OrderProducts.RemoveRange(existingProducts);

            var newOrderProducts = productIds.Select(pid => new OrderProduct
            {
                OrderId = id,
                ProductId = pid
            });

            _context.OrderProducts.AddRange(newOrderProducts);
            _context.SaveChanges();

            return (order, null);
        }

        public bool DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return false;

            var orderProducts = _context.OrderProducts.Where(op => op.OrderId == id);
            _context.OrderProducts.RemoveRange(orderProducts);

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return true;
        }

    }
}
