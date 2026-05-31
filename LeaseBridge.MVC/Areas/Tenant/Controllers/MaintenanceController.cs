using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    [Route("Tenant/[controller]/[action]")]
    [Authorize(Roles = "Tenant")]
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        private const int SubmittedStatusId = 1;

        public MaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tenant/Maintenance/MyRequests
        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return View(new List<TenantMaintenanceViewModel>());
            }

            var requests = await _context.MaintenanceRequests
                .Where(r => r.TenantId == tenant.UserId)
                .Select(r => new TenantMaintenanceViewModel
                {
                    RequestId = r.RequestId,
                    TicketNumber = r.TicketNumber,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,

                    CategoryName = _context.MaintenanceCategories
                        .Where(c => c.CategoryId == r.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() ?? "General Maintenance",

                    PriorityName = _context.PriorityTypes
                        .Where(p => p.PriorityId == r.PriorityId)
                        .Select(p => p.Name)
                        .FirstOrDefault() ?? "Medium",

                    StatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == r.StatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "Submitted"
                })
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(requests);
        }

        // GET: /Tenant/Maintenance/RaiseTicket
        [HttpGet]
        public async Task<IActionResult> RaiseTicket()
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(
                    actionName: nameof(MyRequests),
                    controllerName: "Maintenance",
                    routeValues: new { area = "Tenant" }
                );
            }

            var activeLease = await _context.Leases
                .Include(l => l.Unit)
                .FirstOrDefaultAsync(l => l.TenantId == tenant.UserId && l.IsActive);

            if (activeLease == null)
            {
                TempData["ErrorMessage"] = "You do not have an active lease, so you cannot raise a maintenance request.";
                return RedirectToAction(
                    actionName: nameof(MyRequests),
                    controllerName: "Maintenance",
                    routeValues: new { area = "Tenant" }
                );
            }

            ViewBag.TenantName = tenant.FirstName + " " + tenant.LastName;
            ViewBag.UnitNumber = activeLease.Unit != null
                ? activeLease.Unit.UnitNumber
                : activeLease.UnitId.ToString();

            await LoadDropdownsAsync();

            return View();
        }

        // POST: /Tenant/Maintenance/RaiseTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RaiseTicket(string title, string description, int categoryId, int priorityId)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(
                    actionName: nameof(MyRequests),
                    controllerName: "Maintenance",
                    routeValues: new { area = "Tenant" }
                );
            }

            var activeLease = await _context.Leases
                .Include(l => l.Unit)
                .FirstOrDefaultAsync(l => l.TenantId == tenant.UserId && l.IsActive);

            if (activeLease == null)
            {
                TempData["ErrorMessage"] = "You do not have an active lease, so you cannot raise a maintenance request.";
                return RedirectToAction(
                    actionName: nameof(MyRequests),
                    controllerName: "Maintenance",
                    routeValues: new { area = "Tenant" }
                );
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("title", "Title is required.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                ModelState.AddModelError("description", "Description is required.");
            }

            var categoryExists = await _context.MaintenanceCategories
                .AnyAsync(c => c.CategoryId == categoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError("categoryId", "Please select a valid category.");
            }

            var priorityExists = await _context.PriorityTypes
                .AnyAsync(p => p.PriorityId == priorityId);

            if (!priorityExists)
            {
                ModelState.AddModelError("priorityId", "Please select a valid priority.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.TenantName = tenant.FirstName + " " + tenant.LastName;
                ViewBag.UnitNumber = activeLease.Unit != null
                    ? activeLease.Unit.UnitNumber
                    : activeLease.UnitId.ToString();

                await LoadDropdownsAsync();

                return View();
            }

            var request = new MaintenanceRequest
            {
                TenantId = tenant.UserId,
                UnitId = activeLease.UnitId,
                CategoryId = categoryId,
                PriorityId = priorityId,
                StatusId = SubmittedStatusId,
                Title = title.Trim(),
                Description = description.Trim(),
                TicketNumber = GenerateTicketNumber(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync();

            var notification = new Notification
            {
                UserId = tenant.UserId,
                MaintenanceRequestId = request.RequestId,
                Message = $"Your maintenance request {request.TicketNumber} has been submitted successfully.",
                NotificationType = "Maintenance Submitted",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Ticket {request.TicketNumber} submitted successfully.";

            return RedirectToAction(
                actionName: nameof(MyRequests),
                controllerName: "Maintenance",
                routeValues: new { area = "Tenant" }
            );
        }

        // GET: /Tenant/Maintenance/Details/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(
                    actionName: nameof(MyRequests),
                    controllerName: "Maintenance",
                    routeValues: new { area = "Tenant" }
                );
            }

            var request = await _context.MaintenanceRequests
                .Where(r => r.RequestId == id && r.TenantId == tenant.UserId)
                .Select(r => new TenantMaintenanceViewModel
                {
                    RequestId = r.RequestId,
                    TicketNumber = r.TicketNumber,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,

                    CategoryName = _context.MaintenanceCategories
                        .Where(c => c.CategoryId == r.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() ?? "General Maintenance",

                    PriorityName = _context.PriorityTypes
                        .Where(p => p.PriorityId == r.PriorityId)
                        .Select(p => p.Name)
                        .FirstOrDefault() ?? "Medium",

                    StatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == r.StatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "Submitted"
                })
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return NotFound();
            }

            request.Updates = await _context.MaintenanceUpdates
                .Where(u => u.RequestId == id)
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new TenantMaintenanceUpdateViewModel
                {
                    UpdateId = u.UpdateId,
                    Notes = u.Notes ?? "",
                    CreatedAt = u.CreatedAt,

                    OldStatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == u.OldStatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "N/A",

                    NewStatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == u.NewStatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "Unknown",

                    UpdatedByName = _context.AppUsers
                        .Where(user => user.UserId == u.UpdatedBy)
                        .Select(user => user.FirstName + " " + user.LastName)
                        .FirstOrDefault() ?? "System"
                })
                .ToListAsync();

            return View(request);
        }

        private async Task LoadDropdownsAsync()
        {
            ViewBag.Categories = await _context.MaintenanceCategories
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Priorities = await _context.PriorityTypes
                .OrderBy(p => p.PriorityId)
                .ToListAsync();
        }

        private static string GenerateTicketNumber()
        {
            return "TKT-" + DateTime.UtcNow.ToString("yyyyMMdd") + "-" +
                   Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();
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

    public class TenantMaintenanceViewModel
    {
        public int RequestId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public List<TenantMaintenanceUpdateViewModel> Updates { get; set; } = new();
    }

    public class TenantMaintenanceUpdateViewModel
    {
        public int UpdateId { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string OldStatusName { get; set; } = string.Empty;

        public string NewStatusName { get; set; } = string.Empty;

        public string UpdatedByName { get; set; } = string.Empty;
    }
}