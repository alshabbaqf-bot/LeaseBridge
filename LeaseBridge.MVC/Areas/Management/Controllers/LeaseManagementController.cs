using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    //[Authorize(Roles = "Property Manager")]
    public class LeaseManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        private const int ActiveLeaseStatusId = 2;
        private const int RenewalLeaseStatusId = 4;
        private const int TerminatedLeaseStatusId = 5;

        private const int AvailableUnitStatusId = 1;
        private const int OccupiedUnitStatusId = 3;

        public LeaseManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Management/LeaseManagement/Index
        public async Task<IActionResult> Index()
        {
            var leases = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                    .ThenInclude(u => u.Property)
                .Include(l => l.Status)
                .OrderByDescending(l => l.IsActive)
                .ThenBy(l => l.EndDate)
                .ToListAsync();

            return View(leases);
        }

        // GET: /Management/LeaseManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                    .ThenInclude(u => u.Property)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            return View(lease);
        }

        // GET: /Management/LeaseManagement/Renew/5
        public async Task<IActionResult> Renew(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                    .ThenInclude(u => u.Property)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            if (!lease.IsActive)
            {
                TempData["ErrorMessage"] = "Only active leases can be renewed.";
                return RedirectToAction(nameof(Index));
            }

            return View(lease);
        }

        // POST: /Management/LeaseManagement/Renew/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Renew(int id, DateTime newEndDate)
        {
            var lease = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            if (!lease.IsActive)
            {
                TempData["ErrorMessage"] = "Only active leases can be renewed.";
                return RedirectToAction(nameof(Index));
            }

            if (newEndDate.Date <= lease.EndDate.Date)
            {
                ModelState.AddModelError("newEndDate", "New end date must be after the current lease end date.");
                return View(lease);
            }

            lease.EndDate = newEndDate;
            lease.StatusId = RenewalLeaseStatusId;
            lease.IsActive = true;

            var notification = new Notification
            {
                UserId = lease.TenantId,
                Message = $"Your lease for Unit {lease.Unit.UnitNumber} has been renewed until {newEndDate:dd MMM yyyy}.",
                NotificationType = "Lease Renewed",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Lease renewed successfully and tenant was notified.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Management/LeaseManagement/Terminate/5
        public async Task<IActionResult> Terminate(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                    .ThenInclude(u => u.Property)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            if (!lease.IsActive)
            {
                TempData["ErrorMessage"] = "This lease is already inactive.";
                return RedirectToAction(nameof(Index));
            }

            return View(lease);
        }

        // POST: /Management/LeaseManagement/Terminate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmTerminate(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            if (!lease.IsActive)
            {
                TempData["ErrorMessage"] = "This lease is already inactive.";
                return RedirectToAction(nameof(Index));
            }

            lease.IsActive = false;
            lease.Unit.StatusId = AvailableUnitStatusId;
            lease.EndDate = DateTime.UtcNow;

            if (lease.Unit != null)
            {
                lease.Unit.StatusId = AvailableUnitStatusId;
            }

            var notification = new Notification
            {
                UserId = lease.TenantId,
                Message = $"Your lease for Unit {lease.Unit.UnitNumber} has been terminated.",
                NotificationType = "Lease Terminated",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Lease terminated successfully. The unit is now available.";
            return RedirectToAction(nameof(Index));
        }
    }
}