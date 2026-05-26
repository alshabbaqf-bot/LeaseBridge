using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    //[Authorize(Roles = "Property Manager")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Application status IDs
        private const int PendingApplicationStatusId = 1;
        private const int ApprovedApplicationStatusId = 2;
        private const int RejectedApplicationStatusId = 3;

        // Unit status IDs
        private const int AvailableUnitStatusId = 1;
        private const int OccupiedUnitStatusId = 2;

        // Lease status IDs
        private const int ActiveLeaseStatusId = 1;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Management/Dashboard
        public async Task<IActionResult> Index()
        {
            var pendingApps = await _context.Applications
                .Include(a => a.Tenant)
                .Include(a => a.Unit)
                    .ThenInclude(u => u.Property)
                .Where(a => a.StatusId == PendingApplicationStatusId)
                .OrderByDescending(a => a.ApplicationDate)
                .ToListAsync();

            return View(pendingApps);
        }

        // POST: Management/Dashboard/ApproveApplication/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveApplication(int id)
        {
            var app = await _context.Applications
                .Include(a => a.Unit)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (app == null)
            {
                return NotFound();
            }

            if (app.StatusId != PendingApplicationStatusId)
            {
                TempData["ErrorMessage"] = "This application has already been processed.";
                return RedirectToAction(nameof(Index));
            }

            if (app.Unit == null)
            {
                TempData["ErrorMessage"] = "The selected unit could not be found.";
                return RedirectToAction(nameof(Index));
            }

            if (app.Unit.StatusId != AvailableUnitStatusId)
            {
                TempData["ErrorMessage"] = "This unit is no longer available.";
                return RedirectToAction(nameof(Index));
            }

            var existingActiveLease = await _context.Leases.AnyAsync(l =>
                l.UnitId == app.UnitId &&
                l.IsActive);

            if (existingActiveLease)
            {
                TempData["ErrorMessage"] = "This unit already has an active lease.";
                return RedirectToAction(nameof(Index));
            }

            app.StatusId = ApprovedApplicationStatusId;
            app.UpdatedAt = DateTime.UtcNow;

            app.Unit.StatusId = OccupiedUnitStatusId;

            var lease = new Lease
            {
                TenantId = app.TenantId,
                UnitId = app.UnitId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(1),
                StatusId = ActiveLeaseStatusId,
                IsActive = true
            };

            _context.Leases.Add(lease);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Application approved and lease created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Management/Dashboard/RejectApplication/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectApplication(int id)
        {
            var app = await _context.Applications.FindAsync(id);

            if (app == null)
            {
                return NotFound();
            }

            if (app.StatusId != PendingApplicationStatusId)
            {
                TempData["ErrorMessage"] = "This application has already been processed.";
                return RedirectToAction(nameof(Index));
            }

            app.StatusId = RejectedApplicationStatusId;
            app.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Application rejected successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}