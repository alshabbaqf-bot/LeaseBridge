using LeaseBridge.API.Data;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index(string? search)

        {

            var unitsQuery = _context.Units

                .Include(u => u.Property)

                .Include(u => u.Status)

                .Where(u =>

                    u.Status.Name == "Vacant" ||

                    u.Status.Name == "Available")

                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))

            {

                var keyword = search.Trim();

                unitsQuery = unitsQuery.Where(u =>

                    u.UnitNumber.Contains(keyword) ||

                    u.Property.Name.Contains(keyword));

            }

            var units = await unitsQuery

                .OrderBy(u => u.RentAmount)

                .ToListAsync();

            ViewBag.Search = search;

            return View(units);

        }

        // GET: /PublicUnits/Details/5

        public async Task<IActionResult> Details(int id)

        {

            var unit = await _context.Units

                .Include(u => u.Property)

                .Include(u => u.Status)

                .FirstOrDefaultAsync(u =>

                    u.UnitId == id &&

                    (u.Status.Name == "Vacant" || u.Status.Name == "Available"));

            if (unit == null)

            {

                return NotFound();

            }

            return View(unit);

        }

    }

}
