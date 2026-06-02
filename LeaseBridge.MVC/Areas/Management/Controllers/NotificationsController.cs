using LeaseBridge.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Property Manager")]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Management/Notifications
        public async Task<IActionResult> Index()
        {
            var notifications = await _context.Notifications
                .OrderBy(n => n.IsRead)
                .ThenByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        // GET: /Management/Notifications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == id);

            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: /Management/Notifications/MarkAsRead/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == id);

            if (notification == null)
            {
                return NotFound();
            }

            notification.IsRead = true;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Notification marked as read.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Management/Notifications/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == id);

            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Notification deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}