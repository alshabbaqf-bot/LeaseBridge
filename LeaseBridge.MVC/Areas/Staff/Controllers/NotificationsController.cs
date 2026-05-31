using LeaseBridge.API.Data;

using LeaseBridge.API.Models;

// using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Staff.Controllers

{

    [Area("Staff")]

    // [Authorize(Roles = "Staff")]

    public class NotificationsController : Controller

    {

        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)

        {

            _context = context;

        }

        // GET: /Staff/Notifications

        public async Task<IActionResult> Index()

        {

            var staff = await GetCurrentStaffAsync();

            if (staff == null)

            {

                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";

                return View(new List<Notification>());

            }

            var notifications = await _context.Notifications

                .Where(n => n.UserId == staff.UserId)

                .OrderBy(n => n.IsRead)

                .ThenByDescending(n => n.CreatedAt)

                .ToListAsync();

            return View(notifications);

        }

        // GET: /Staff/Notifications/Details/5

        public async Task<IActionResult> Details(int id)

        {

            var staff = await GetCurrentStaffAsync();

            if (staff == null)

            {

                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";

                return RedirectToAction(

                    actionName: "Index",

                    controllerName: "Notifications",

                    routeValues: new { area = "Staff" }

                );

            }

            var notification = await _context.Notifications

                .FirstOrDefaultAsync(n =>

                    n.NotificationId == id &&

                    n.UserId == staff.UserId);

            if (notification == null)

            {

                return NotFound();

            }

            return View(notification);

        }

        // POST: /Staff/Notifications/MarkAsRead/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> MarkAsRead(int id)

        {

            var staff = await GetCurrentStaffAsync();

            if (staff == null)

            {

                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";

                return RedirectToAction(

                    actionName: "Index",

                    controllerName: "Notifications",

                    routeValues: new { area = "Staff" }

                );

            }

            var notification = await _context.Notifications

                .FirstOrDefaultAsync(n =>

                    n.NotificationId == id &&

                    n.UserId == staff.UserId);

            if (notification == null)

            {

                return NotFound();

            }

            notification.IsRead = true;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Notification marked as read.";

            return RedirectToAction(

                actionName: "Index",

                controllerName: "Notifications",

                routeValues: new { area = "Staff" }

            );

        }

        private async Task<AppUser?> GetCurrentStaffAsync()

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
