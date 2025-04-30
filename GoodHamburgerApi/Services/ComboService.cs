using GoodHamburger.Data;
using GoodHamburgerApi.Models;
using GoodHamburgerApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburgerApi.Services
{
    public class ComboService
    {
        private readonly GoodHamburgerContext _context;

        public ComboService(GoodHamburgerContext context)
        {
            _context = context;
        }
        public IEnumerable<Combo> GetAll()
        {
            return _context.Combos.ToList();
        }

        public Combo? GetComboById(int id)
        {
            return _context.Combos.FirstOrDefault(c => c.Id == id);
        }
    }
}
