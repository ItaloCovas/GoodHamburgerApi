using GoodHamburger.Data;
using GoodHamburgerApi.Models.Enums;
using GoodHamburgerApi.Models;

namespace GoodHamburgerApi.Services
{
    public class ProductService
    {
        private readonly GoodHamburgerContext _context;

        public ProductService(GoodHamburgerContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetSandwiches()
        {
            return _context.Products.Where(p => p.Type == ProductType.Sandwich);
        }

        public IEnumerable<Product> GetExtras()
        {
            return _context.Products.Where(p => p.Type == ProductType.Extra);
        }
    }
}
