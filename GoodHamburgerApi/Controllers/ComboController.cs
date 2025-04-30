using GoodHamburgerApi.Models;
using GoodHamburgerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburgerApi.Controllers
{
    [ApiController]
    [Route("combo")]
    public class ComboController : ControllerBase
    {
        private readonly ComboService _comboService;

        public ComboController(ComboService comboService)
        {
            _comboService = comboService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Combo>> GetAll()
        {
            var combos = _comboService.GetAll();
            return Ok(combos);
        }

        [HttpGet("{id}")]
        public ActionResult<Combo> GetById(int id)
        {
            var combo = _comboService.GetComboById(id);

            if (combo == null)
                return NotFound(new { message = "Combo not found." });

            return Ok(combo);
        }
    }
}
