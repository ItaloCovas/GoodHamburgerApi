using GoodHamburger.Data;
using GoodHamburgerApi.Models.Enums;
using GoodHamburgerApi.Models;

namespace GoodHamburger.Seed
{
    public static class DbSeed
    {
        public static void SeedDatabase(GoodHamburgerContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich },
                    new Product { Name = "X Egg", Price = 4.50m, Type = ProductType.Sandwich },
                    new Product { Name = "X Bacon", Price = 7.00m, Type = ProductType.Sandwich },
                    new Product { Name = "Fries", Price = 2.00m, Type = ProductType.Extra },
                    new Product { Name = "Soft Drink", Price = 2.50m, Type = ProductType.Extra }
                );

                context.SaveChanges();

                context.Combos.AddRange(
                    new Combo
                    {
                        Name = "Sandwich + Fries + Soft Drink",
                        DiscountPercent = 20,
                        ProductsIds = string.Join(",",
                            context.Products.Where(p => p.Name == "X Burger").Select(p => p.Id).FirstOrDefault(),
                            context.Products.Where(p => p.Name == "Fries").Select(p => p.Id).FirstOrDefault(),
                            context.Products.Where(p => p.Name == "Soft Drink").Select(p => p.Id).FirstOrDefault())
                    },
                    new Combo
                    {
                        Name = "Sandwich + Soft Drink",
                        DiscountPercent = 15,
                        ProductsIds = string.Join(",",
                            context.Products.Where(p => p.Name == "X Burger").Select(p => p.Id).FirstOrDefault(),
                            context.Products.Where(p => p.Name == "Soft Drink").Select(p => p.Id).FirstOrDefault())
                    },
                    new Combo
                    {
                        Name = "Sandwich + Fries",
                        DiscountPercent = 10,
                        ProductsIds = string.Join(",",
                            context.Products.Where(p => p.Name == "X Burger").Select(p => p.Id).FirstOrDefault(),
                            context.Products.Where(p => p.Name == "Fries").Select(p => p.Id).FirstOrDefault())
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
