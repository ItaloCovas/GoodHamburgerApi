using GoodHamburgerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Data
{
    public class GoodHamburgerContext : DbContext
    {
        public GoodHamburgerContext(DbContextOptions<GoodHamburgerContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Combo> Combos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OrderProduct key (orderId + productId)
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });
        }
    }
}
