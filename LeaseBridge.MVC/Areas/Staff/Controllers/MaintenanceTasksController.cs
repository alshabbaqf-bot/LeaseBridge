using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    public class MaintenanceTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Staff/MaintenanceTasks/MyTasks
        public async Task<IActionResult> MyTasks()
        {
            var staff = await GetCurrentStaffAsync();

            if (staff == null)
            {
                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";
                return View(new List<StaffMaintenanceTaskViewModel>());
            }

            var tasks = await _context.MaintenanceAssignments
                .Where(a => a.StaffId == staff.UserId)
                .Select(a => new StaffMaintenanceTaskViewModel
                {
                    AssignmentId = a.AssignmentId,
                    RequestId = a.RequestId,

                    TicketNumber = a.Request.TicketNumber,
                    Title = a.Request.Title,
                    Description = a.Request.Description,
                    CreatedAt = a.Request.CreatedAt,
                    AssignedDate = a.AssignedDate,

                    TenantName = a.Request.Tenant.FirstName + " " + a.Request.Tenant.LastName,
                    UnitNumber = a.Request.Unit.UnitNumber,
                    CategoryName = a.Request.Category.Name,
                    PriorityName = a.Request.Priority.Name,
                    StatusName = a.Request.Status.Name
                })
                .OrderByDescending(t => t.AssignedDate)
                .ToListAsync();

            return View(tasks);
        }

        // GET: /Staff/MaintenanceTasks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var staff = await GetCurrentStaffAsync();

            if (staff == null)
            {
                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyTasks));
            }

            var task = await _context.MaintenanceAssignments
                .Where(a => a.RequestId == id && a.StaffId == staff.UserId)
                .Select(a => new StaffMaintenanceTaskViewModel
                {
                    AssignmentId = a.AssignmentId,
                    RequestId = a.RequestId,

                    TicketNumber = a.Request.TicketNumber,
                    Title = a.Request.Title,
                    Description = a.Request.Description,
                    CreatedAt = a.Request.CreatedAt,
                    AssignedDate = a.AssignedDate,

                    TenantName = a.Request.Tenant.FirstName + " " + a.Request.Tenant.LastName,
                    UnitNumber = a.Request.Unit.UnitNumber,
                    CategoryName = a.Request.Category.Name,
                    PriorityName = a.Request.Priority.Name,
                    StatusName = a.Request.Status.Name
                })
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            task.Updates = await _context.MaintenanceUpdates
                .Where(u => u.RequestId == id)
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new StaffMaintenanceUpdateViewModel
                {
                    UpdateId = u.UpdateId,
                    Notes = u.Notes ?? "",
                    CreatedAt = u.CreatedAt,

                    StatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == u.NewStatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "Unknown",

                    StaffName = _context.AppUsers
                        .Where(user => user.UserId == u.UpdatedBy)
                        .Select(user => user.FirstName + " " + user.LastName)
                        .FirstOrDefault() ?? "Unknown Staff"
                })
                .ToListAsync();

            return View(task);
        }

        // GET: /Staff/MaintenanceTasks/UpdateProgress/5
        // GET: /Staff/MaintenanceTasks/UpdateProgress/5
        public async Task<IActionResult> UpdateProgress(int id)
        {
            var staff = await GetCurrentStaffAsync();

            if (staff == null)
            {
                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyTasks));
            }

            var assignmentExists = await _context.MaintenanceAssignments
                .AnyAsync(a => a.RequestId == id && a.StaffId == staff.UserId);

            if (!assignmentExists)
            {
                TempData["ErrorMessage"] = "This task is not assigned to the current staff user.";
                return RedirectToAction(nameof(MyTasks));
            }

            var request = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (request == null)
            {
                TempData["ErrorMessage"] = "Maintenance request was not found.";
                return RedirectToAction(nameof(MyTasks));
            }

            ViewBag.RequestId = request.RequestId;
            ViewBag.TicketNumber = request.TicketNumber;
            ViewBag.Title = request.Title;
            ViewBag.CurrentStatusId = request.StatusId;

            ViewBag.Statuses = await _context.MaintenanceStatuses
                .OrderBy(s => s.StatusId)
                .ToListAsync();

            return View();
        }
        // POST: /Staff/MaintenanceTasks/UpdateProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProgress(int requestId, int statusId, string notes)
        {
            var staff = await GetCurrentStaffAsync();

            if (staff == null)
            {
                TempData["ErrorMessage"] = "Staff profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyTasks));
            }

            var assignmentExists = await _context.MaintenanceAssignments
                .AnyAsync(a => a.RequestId == requestId && a.StaffId == staff.UserId);

            if (!assignmentExists)
            {
                TempData["ErrorMessage"] = "This maintenance task is not assigned to the current staff user.";
                return RedirectToAction(nameof(MyTasks));
            }

            var request = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null)
            {
                TempData["ErrorMessage"] = "Maintenance request was not found.";
                return RedirectToAction(nameof(MyTasks));
            }

            var statusExists = await _context.MaintenanceStatuses
                .AnyAsync(s => s.StatusId == statusId);

            if (!statusExists)
            {
                ModelState.AddModelError("", "Please select a valid status.");
            }

            if (string.IsNullOrWhiteSpace(notes))
            {
                ModelState.AddModelError("", "Progress notes are required.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.RequestId = request.RequestId;
                ViewBag.TicketNumber = request.TicketNumber;
                ViewBag.Title = request.Title;
                ViewBag.CurrentStatusId = request.StatusId;

                ViewBag.Statuses = await _context.MaintenanceStatuses
                    .OrderBy(s => s.StatusId)
                    .ToListAsync();

                return View();
            }

            var oldStatusId = request.StatusId;

            request.StatusId = statusId;
            request.UpdatedAt = DateTime.UtcNow;

            var update = new MaintenanceUpdate
            {
                RequestId = requestId,
                OldStatusId = oldStatusId,
                NewStatusId = statusId,
                UpdatedBy = staff.UserId,
                Notes = notes.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.MaintenanceUpdates.Add(update);

            var tenantNotification = new Notification
            {
                UserId = request.TenantId,
                MaintenanceRequestId = request.RequestId,
                Message = $"Your maintenance request {request.TicketNumber} was updated by staff.",
                NotificationType = "Maintenance Progress Update",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(tenantNotification);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Task progress updated successfully.";
            return LocalRedirect("/Staff/MaintenanceTasks/MyTasks");
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
        //test
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private async Task<AppUser?> GetCurrentStaffAsync()
        //{
        ////    // TEMPORARY TESTING ONLY:
        ////    // Replace this UserId with a real staff UserId from your AppUsers seed data.
        //const int testStaffUserId = 13;

        // return await _context.AppUsers
        //     .FirstOrDefaultAsync(u => u.UserId == testStaffUserId);
        //}
    }

    public class StaffMaintenanceTaskViewModel
    {
        public int AssignmentId { get; set; }

        public int RequestId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime AssignedDate { get; set; }

        public string TenantName { get; set; } = string.Empty;

        public string UnitNumber { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public List<StaffMaintenanceUpdateViewModel> Updates { get; set; } = new();
    }

    public class StaffMaintenanceUpdateViewModel
    {
        public int UpdateId { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string StatusName { get; set; } = string.Empty;

        public string StaffName { get; set; } = string.Empty;
    }
}