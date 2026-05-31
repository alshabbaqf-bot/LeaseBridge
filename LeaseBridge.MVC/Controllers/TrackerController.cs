using LeaseBridge.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Controllers
{
    public class TrackerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrackerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tracker
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Tracker/CheckStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckStatus(int applicationId)
        {
            if (applicationId <= 0)
            {
                ViewBag.Status = "Please enter a valid application reference ID.";
                return View("Index");
            }

            var application = await _context.Applications
                .Include(a => a.Status)
                .Include(a => a.Unit)
                    .ThenInclude(u => u.Property)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null)
            {
                ViewBag.Status = "Application record was not found.";
                return View("Index");
            }

            var unitText = application.Unit != null
                ? $"Unit {application.Unit.UnitNumber}"
                : "Selected unit";

            var propertyText = application.Unit?.Property != null
                ? application.Unit.Property.Name
                : "Property not available";

            var statusText = application.Status != null
                ? application.Status.Name
                : $"Status ID {application.StatusId}";

            ViewBag.Status = $"{unitText} - {propertyText} | Current Status: {statusText}";

            return View("Index");
        }
    }
}