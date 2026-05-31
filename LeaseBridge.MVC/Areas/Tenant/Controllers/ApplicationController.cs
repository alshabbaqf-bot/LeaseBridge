using LeaseBridge.API.Data;

using LeaseBridge.API.Models;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Tenant.Controllers

{

    [Area("Tenant")]

    [Authorize(Roles = "Tenant")]

    public class ApplicationController : Controller

    {

        private readonly ApplicationDbContext _context;

        private const int PendingStatusId = 1;

        public ApplicationController(ApplicationDbContext context)

        {

            _context = context;

        }

        // GET: /Tenant/Application/Apply?unitId=1

        public async Task<IActionResult> Apply(int unitId)

        {

            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)

            {

                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";

                return RedirectToAction(

                    actionName: "Index",

                    controllerName: "AvailableUnits",

                    routeValues: new { area = "Tenant" }

                );

            }

            var unit = await _context.Units

                .Include(u => u.Property)

                .FirstOrDefaultAsync(u => u.UnitId == unitId);

            if (unit == null)

            {

                return NotFound();

            }

            var alreadyApplied = await _context.Applications

                .AnyAsync(a =>

                    a.UnitId == unitId &&

                    a.TenantId == tenant.UserId &&

                    a.StatusId == PendingStatusId);

            if (alreadyApplied)

            {

                TempData["ErrorMessage"] = "You already have a pending application for this unit.";

                return RedirectToAction(

                    actionName: "Index",

                    controllerName: "AvailableUnits",

                    routeValues: new { area = "Tenant" }

                );

            }

            ViewBag.UnitId = unit.UnitId;

            ViewBag.UnitNumber = unit.UnitNumber;

            ViewBag.PropertyName = unit.Property != null ? unit.Property.Name : "N/A";

            ViewBag.RentAmount = unit.RentAmount;

            ViewBag.TenantName = tenant.FirstName + " " + tenant.LastName;

            return View();

        }

        // POST: /Tenant/Application/SubmitApplication

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SubmitApplication(int unitId)

        {

            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)

            {

                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";

                return RedirectToAction(

                    actionName: "Index",

                    controllerName: "AvailableUnits",

                    routeValues: new { area = "Tenant" }

                );

            }

            var unitExists = await _context.Units.AnyAsync(u => u.UnitId == unitId);

            if (!unitExists)

            {

                return NotFound();

            }

            var alreadyApplied = await _context.Applications

                .AnyAsync(a =>

                    a.UnitId == unitId &&

                    a.TenantId == tenant.UserId &&

                    a.StatusId == PendingStatusId);

            if (alreadyApplied)

            {

                TempData["ErrorMessage"] = "You already have a pending application for this unit.";

                return RedirectToAction(

                    actionName: "Index",

                    controllerName: "AvailableUnits",

                    routeValues: new { area = "Tenant" }

                );

            }

            var application = new Application

            {

                TenantId = tenant.UserId,

                UnitId = unitId,

                ApplicationDate = DateTime.UtcNow,

                StatusId = PendingStatusId,

                CreatedAt = DateTime.UtcNow,

                UpdatedAt = null

            };

            _context.Applications.Add(application);

            await _context.SaveChangesAsync();

            var notification = new Notification

            {

                UserId = tenant.UserId,

                ApplicationId = application.ApplicationId,

                Message = "Your rental application has been submitted and is waiting for management review.",

                NotificationType = "Application Submitted",

                IsRead = false,

                CreatedAt = DateTime.UtcNow

            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your rental application has been submitted successfully.";

            return RedirectToAction(

                actionName: nameof(MyApplications),

                controllerName: "Application",

                routeValues: new { area = "Tenant" }

            );

        }

        // GET: /Tenant/Application/MyApplications

        public async Task<IActionResult> MyApplications()

        {

            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)

            {

                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";

                return View(new List<Application>());

            }

            var applications = await _context.Applications

                .Include(a => a.Unit)

                    .ThenInclude(u => u.Property)

                .Include(a => a.Status)

                .Where(a => a.TenantId == tenant.UserId)

                .OrderByDescending(a => a.ApplicationDate)

                .ToListAsync();

            return View(applications);

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
