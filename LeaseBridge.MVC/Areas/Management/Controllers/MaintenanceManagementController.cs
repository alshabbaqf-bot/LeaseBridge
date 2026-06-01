using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
   // [Authorize(Roles = "Property Manager")]
    public class MaintenanceManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Management/MaintenanceManagement/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var allTickets = await _context.MaintenanceRequests
                .Select(r => new ManagementTicketViewModel
                {
                    RequestId = r.RequestId,
                    TicketNumber = r.TicketNumber,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,

                    TenantName = _context.AppUsers
                        .Where(u => u.UserId == r.TenantId)
                        .Select(u => u.FirstName + " " + u.LastName)
                        .FirstOrDefault() ?? "Unknown Tenant",

                    UnitName = _context.Units
                        .Where(u => u.UnitId == r.UnitId)
                        .Select(u => u.UnitNumber)
                        .FirstOrDefault() ?? "N/A",

                    CategoryName = _context.MaintenanceCategories
                        .Where(c => c.CategoryId == r.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() ?? "General",

                    PriorityName = _context.PriorityTypes
                        .Where(p => p.PriorityId == r.PriorityId)
                        .Select(p => p.Name)
                        .FirstOrDefault() ?? "Medium",

                    StatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == r.StatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "Submitted",

                    AssignedStaffName = _context.MaintenanceAssignments
                        .Where(a => a.RequestId == r.RequestId)
                        .OrderByDescending(a => a.AssignedDate)
                        .Select(a => a.Staff.FirstName + " " + a.Staff.LastName)
                        .FirstOrDefault() ?? "Unassigned",

                    AssignedDate = _context.MaintenanceAssignments
                        .Where(a => a.RequestId == r.RequestId)
                        .OrderByDescending(a => a.AssignedDate)
                        .Select(a => (DateTime?)a.AssignedDate)
                        .FirstOrDefault()
                })
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return View(allTickets);
        }

        // GET: /Management/MaintenanceManagement/ProcessTicket/5
        public async Task<IActionResult> ProcessTicket(int id)
        {
            var ticket = await _context.MaintenanceRequests
                .Where(r => r.RequestId == id)
                .Select(r => new ManagementProcessTicketViewModel
                {
                    RequestId = r.RequestId,
                    TicketNumber = r.TicketNumber,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,
                    StatusId = r.StatusId,
                    CategoryId = r.CategoryId,

                    TenantName = _context.AppUsers
                        .Where(u => u.UserId == r.TenantId)
                        .Select(u => u.FirstName + " " + u.LastName)
                        .FirstOrDefault() ?? "Unknown Tenant",

                    UnitName = _context.Units
                        .Where(u => u.UnitId == r.UnitId)
                        .Select(u => u.UnitNumber)
                        .FirstOrDefault() ?? "N/A",

                    CategoryName = _context.MaintenanceCategories
                        .Where(c => c.CategoryId == r.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() ?? "General",

                    PriorityName = _context.PriorityTypes
                        .Where(p => p.PriorityId == r.PriorityId)
                        .Select(p => p.Name)
                        .FirstOrDefault() ?? "Medium",

                    CurrentStatusName = _context.MaintenanceStatuses
                        .Where(s => s.StatusId == r.StatusId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "Submitted",

                    AssignedStaffId = _context.MaintenanceAssignments
                        .Where(a => a.RequestId == r.RequestId)
                        .OrderByDescending(a => a.AssignedDate)
                        .Select(a => (int?)a.StaffId)
                        .FirstOrDefault(),

                    AssignedStaffName = _context.MaintenanceAssignments
                        .Where(a => a.RequestId == r.RequestId)
                        .OrderByDescending(a => a.AssignedDate)
                        .Select(a => a.Staff.FirstName + " " + a.Staff.LastName)
                        .FirstOrDefault() ?? "Unassigned"
                })
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return NotFound();
            }

            await LoadDropdownsAsync(
                selectedStatusId: ticket.StatusId,
                selectedStaffId: ticket.AssignedStaffId,
                categoryId: ticket.CategoryId
            );

            return View(ticket);
        }

        // POST: /Management/MaintenanceManagement/ProcessTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessTicket(
            int requestId,
            int statusId,
            int? staffId,
            string? managerNotes)
        {
            var ticket = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (ticket == null)
            {
                return NotFound();
            }

            var statusExists = await _context.MaintenanceStatuses
                .AnyAsync(s => s.StatusId == statusId);

            if (!statusExists)
            {
                TempData["ManagementError"] = "Invalid maintenance status selected.";
                return RedirectToAction(nameof(ProcessTicket), new { id = requestId });
            }

            AppUser? selectedStaff = null;

            if (staffId.HasValue)
            {
                selectedStaff = await _context.AppUsers
                    .FirstOrDefaultAsync(u => u.UserId == staffId.Value);

                if (selectedStaff == null)
                {
                    TempData["ManagementError"] = "Selected staff member was not found.";
                    return RedirectToAction(nameof(ProcessTicket), new { id = requestId });
                }

                if (selectedStaff.IsAvailable != true)
                {
                    TempData["ManagementError"] = "Selected staff member is not available.";
                    return RedirectToAction(nameof(ProcessTicket), new { id = requestId });
                }

                var staffHasMatchingSkill = await _context.StaffSkills
                    .AnyAsync(ss =>
                        ss.StaffId == staffId.Value &&
                        ss.CategoryId == ticket.CategoryId);

                if (!staffHasMatchingSkill)
                {
                    TempData["ManagementError"] = "Selected staff member does not have the required skill for this maintenance request.";
                    return RedirectToAction(nameof(ProcessTicket), new { id = requestId });
                }
            }

            var oldStatusId = ticket.StatusId;

            ticket.StatusId = statusId;
            ticket.UpdatedAt = DateTime.UtcNow;

            if (staffId.HasValue)
            {
                var existingAssignment = await _context.MaintenanceAssignments
                    .FirstOrDefaultAsync(a => a.RequestId == requestId);

                if (existingAssignment == null)
                {
                    var assignment = new MaintenanceAssignment
                    {
                        RequestId = requestId,
                        StaffId = staffId.Value,
                        AssignedDate = DateTime.UtcNow
                    };

                    _context.MaintenanceAssignments.Add(assignment);
                }
                else
                {
                    existingAssignment.StaffId = staffId.Value;
                    existingAssignment.AssignedDate = DateTime.UtcNow;
                }

                var staffNotification = new Notification
                {
                    UserId = staffId.Value,
                    MaintenanceRequestId = requestId,
                    Message = $"You have been assigned maintenance ticket {ticket.TicketNumber}.",
                    NotificationType = "Maintenance Assignment",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Notifications.Add(staffNotification);
            }

            var managerUser = await GetCurrentManagerAsync();

            var updaterId = managerUser?.UserId ?? staffId;

            if (updaterId.HasValue && (oldStatusId != statusId || !string.IsNullOrWhiteSpace(managerNotes)))
            {
                var update = new MaintenanceUpdate
                {
                    RequestId = requestId,
                    OldStatusId = oldStatusId,
                    NewStatusId = statusId,
                    UpdatedBy = updaterId.Value,
                    Notes = string.IsNullOrWhiteSpace(managerNotes)
                        ? "Status updated by management."
                        : managerNotes.Trim(),
                    CreatedAt = DateTime.UtcNow
                };

                _context.MaintenanceUpdates.Add(update);
            }

            var tenantNotification = new Notification
            {
                UserId = ticket.TenantId,
                MaintenanceRequestId = requestId,
                Message = $"Your maintenance request {ticket.TicketNumber} has been updated.",
                NotificationType = "Maintenance Update",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(tenantNotification);

            await _context.SaveChangesAsync();

            TempData["ManagementSuccess"] = $"Ticket {ticket.TicketNumber} updated successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        private async Task LoadDropdownsAsync(
            int? selectedStatusId = null,
            int? selectedStaffId = null,
            int? categoryId = null)
        {
            var statuses = await _context.MaintenanceStatuses
                .OrderBy(s => s.StatusId)
                .ToListAsync();

            ViewData["StatusId"] = new SelectList(
                statuses,
                "StatusId",
                "Name",
                selectedStatusId
            );

            var staffRole = await _context.Set<IdentityRole>()
                .FirstOrDefaultAsync(r => r.Name == "Staff");

            var staffMembers = new List<StaffDropdownViewModel>();

            if (staffRole != null)
            {
                var staffIdentityUserIds = await _context.Set<IdentityUserRole<string>>()
                    .Where(ur => ur.RoleId == staffRole.Id)
                    .Select(ur => ur.UserId)
                    .ToListAsync();

                var query = _context.AppUsers
                    .Where(u =>
                        u.IdentityUserId != null &&
                        staffIdentityUserIds.Contains(u.IdentityUserId) &&
                        u.IsAvailable == true);

                if (categoryId.HasValue)
                {
                    query = query.Where(u =>
                        _context.StaffSkills.Any(ss =>
                            ss.StaffId == u.UserId &&
                            ss.CategoryId == categoryId.Value));
                }

                staffMembers = await query
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .Select(u => new StaffDropdownViewModel
                    {
                        UserId = u.UserId,
                        FullName = u.FirstName + " " + u.LastName
                    })
                    .ToListAsync();
            }

            ViewData["StaffId"] = new SelectList(
                staffMembers,
                "UserId",
                "FullName",
                selectedStaffId
            );
        }

        private async Task<AppUser?> GetCurrentManagerAsync()
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

    public class StaffDropdownViewModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;
    }

    public class ManagementTicketViewModel
    {
        public int RequestId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string TenantName { get; set; } = string.Empty;

        public string UnitName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public string AssignedStaffName { get; set; } = string.Empty;

        public DateTime? AssignedDate { get; set; }
    }

    public class ManagementProcessTicketViewModel
    {
        public int RequestId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int StatusId { get; set; }

        public int CategoryId { get; set; }

        public string CurrentStatusName { get; set; } = string.Empty;

        public string TenantName { get; set; } = string.Empty;

        public string UnitName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public int? AssignedStaffId { get; set; }

        public string AssignedStaffName { get; set; } = string.Empty;
    }
}