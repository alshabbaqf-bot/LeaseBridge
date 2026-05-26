using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaseBridge.API.Data;   // FIX: Pulling Data directly from the API project
using LeaseBridge.API.Models; // FIX: Pulling Models directly from the API project

namespace LeaseBridge.MVC.Controllers
{
    public class PublicUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /PublicUnits
        public async Task<IActionResult> Index(string searchString, decimal? maxPrice)
        {
            // Query the active Units database set from the API layer
            var query = _context.Units
                .Include(u => u.Property)
                .Where(u => u.StatusId == 1); // StatusId = 1 maps to 'Available'

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(u => u.Property.Name.Contains(searchString)
                                      || u.Property.Location.Contains(searchString));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(u => u.RentAmount <= maxPrice.Value);
            }

            List<Unit> availableUnits = await query.ToListAsync();
            return View(availableUnits);
        }

        // GET: /PublicUnits/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Unit? unit = await _context.Units
                .Include(u => u.Property)
                .FirstOrDefaultAsync(u => u.UnitId == id);

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }
    }
}