using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;


namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    //[Authorize(Roles = "Property Manager")]
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
                        .FirstOrDefault() ?? "Submitted"
                })
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return View(allTickets);
        }

        // GET: /Management/MaintenanceManagement/ProcessTicket/5
        public async Task<IActionResult> ProcessTicket(int id)
        {
            var ticket = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            ViewBag.Statuses = await _context.MaintenanceStatuses
                .OrderBy(s => s.StatusId)
                .ToListAsync();

            return View(ticket);
        }

        // POST: /Management/MaintenanceManagement/ProcessTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessTicket(int requestId, int statusId)
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

            ticket.StatusId = statusId;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["ManagementSuccess"] = $"Ticket {ticket.TicketNumber} updated successfully.";
            return RedirectToAction(nameof(Dashboard));
        }
    }

    public class ManagementTicketViewModel
    {
        public int RequestId { get; set; }

        public string TicketNumber { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string TenantName { get; set; } = string.Empty;

        public string UnitName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;
    }
}