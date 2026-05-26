using LeaseBridge.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    public class AvailableUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvailableUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tenant/AvailableUnits
        public async Task<IActionResult> Index()
        {
            var availableUnits = await _context.Units
                .Include(u => u.Property)
                .Include(u => u.Status)
                .Where(u =>
                    u.Status.Name == "Vacant" ||
                    u.Status.Name == "Available")
                .OrderBy(u => u.RentAmount)
                .ToListAsync();

            return View(availableUnits);
        }

        // GET: /Tenant/AvailableUnits/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var unit = await _context.Units
                .Include(u => u.Property)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.UnitId == id);

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }
    }
}