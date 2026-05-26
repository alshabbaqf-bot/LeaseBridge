using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    // [Authorize(Roles = "Tenant")]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tenant/Notifications
        public async Task<IActionResult> Index()
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return View(new List<Notification>());
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == tenant.UserId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        // GET: /Tenant/Notifications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(nameof(Index));
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n =>
                    n.NotificationId == id &&
                    n.UserId == tenant.UserId);

            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: /Tenant/Notifications/MarkAsRead/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(nameof(Index));
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n =>
                    n.NotificationId == id &&
                    n.UserId == tenant.UserId);

            if (notification == null)
            {
                return NotFound();
            }

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Notification marked as read.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<AppUser?> GetCurrentTenantAsync()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(identityUserId))
            {
                return null;
            }

            return await _context.AppUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
    }
}