using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // OVERVIEW DASHBOARD
        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview()
        {
            var totalProperties = await _context.Properties.CountAsync();

            var totalUnits = await _context.Units.CountAsync();

            var occupiedUnits = await _context.Units
                .CountAsync(u => u.StatusId == 2);

            var availableUnits = await _context.Units
                .CountAsync(u => u.StatusId == 1);

            var activeLeases = await _context.Leases
                .CountAsync(l => l.IsActive);

            var totalTenants = await _context.AppUsers
                .CountAsync(u =>
                    _context.UserRoles.Any(ur =>
                        ur.UserId == u.IdentityUserId &&
                        _context.Roles.Any(r =>
                            r.Id == ur.RoleId &&
                            r.Name == "Tenant")));

            var totalStaff = await _context.AppUsers
                .CountAsync(u =>
                    _context.UserRoles.Any(ur =>
                        ur.UserId == u.IdentityUserId &&
                        _context.Roles.Any(r =>
                            r.Id == ur.RoleId &&
                            r.Name == "Staff")));

            return Ok(new
            {
                TotalProperties = totalProperties,
                TotalUnits = totalUnits,
                OccupiedUnits = occupiedUnits,
                AvailableUnits = availableUnits,
                ActiveLeases = activeLeases,
                TotalTenants = totalTenants,
                TotalStaff = totalStaff
            });
        }

        // PAYMENT & INVOICE STATISTICS
        [HttpGet("payments")]
        public async Task<IActionResult> GetPaymentStatistics()
        {
            var totalPayments = await _context.Payments
                .CountAsync();

            var paidInvoices = await _context.Invoices
                .CountAsync(i => i.StatusId == 2);

            var pendingInvoices = await _context.Invoices
                .CountAsync(i => i.StatusId == 1);

            var overdueInvoices = await _context.Invoices
                .CountAsync(i => i.StatusId == 3);

            var totalRevenue = await _context.Payments
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            return Ok(new
            {
                TotalPayments = totalPayments,
                PaidInvoices = paidInvoices,
                PendingInvoices = pendingInvoices,
                OverdueInvoices = overdueInvoices,
                TotalRevenue = totalRevenue
            });
        }

        [HttpGet("invoice-status-by-month")]
        public async Task<IActionResult> GetInvoiceStatusByMonth()
        {
            var data = await _context.Invoices
                .GroupBy(i => new
                {
                    i.IssuedDate.Year,
                    i.IssuedDate.Month
                })
                .Select(g => new InvoiceStatusByMonthDto
                {
                    Month = new DateTime(
                        g.Key.Year,
                        g.Key.Month,
                        1).ToString("MMM"),

                    PaidCount =
                        g.Count(i => i.StatusId == 2),

                    PendingCount =
                        g.Count(i => i.StatusId == 1),

                    OverdueCount =
                        g.Count(i => i.StatusId == 3)
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("overdue-invoices")]
        public async Task<IActionResult> GetOverdueInvoices()
        {
            var invoices = await _context.Invoices
                .Where(i => i.StatusId == 3)
                .Select(i => new OverdueInvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceNumber = i.InvoiceNumber,
                    Amount = i.Amount,
                    DueDate = i.DueDate,

                    TenantName =
                        i.Lease.Tenant.FirstName + " " +
                        i.Lease.Tenant.LastName
                })
                .OrderBy(i => i.DueDate)
                .ToListAsync();

            return Ok(invoices);
        }

        // MAINTENANCE STATISTICS
        [HttpGet("maintenance")]
        public async Task<IActionResult> GetMaintenanceStatistics()
        {
            var totalRequests = await _context.MaintenanceRequests
                .CountAsync();

            var openRequests = await _context.MaintenanceRequests
                .CountAsync(r => r.StatusId == 1);

            var inProgressRequests = await _context.MaintenanceRequests
                .CountAsync(r => r.StatusId == 2);

            var completedRequests = await _context.MaintenanceRequests
                .CountAsync(r => r.StatusId == 3);

            var highPriorityRequests = await _context.MaintenanceRequests
                .CountAsync(r => r.PriorityId == 3);

            var totalAssignments = await _context.MaintenanceAssignments
                .CountAsync();

            return Ok(new
            {
                TotalRequests = totalRequests,
                OpenRequests = openRequests,
                InProgressRequests = inProgressRequests,
                CompletedRequests = completedRequests,
                HighPriorityRequests = highPriorityRequests,
                TotalAssignments = totalAssignments
            });
        }

        [HttpGet("high-priority-requests")]
        public async Task<IActionResult> GetHighPriorityRequests()
        {
            var requests = await _context.MaintenanceRequests
                .Where(r => r.PriorityId == 3)
                .Select(r => new HighPriorityRequestDto
                {
                    RequestId = r.RequestId,
                    UnitNumber = r.Unit.UnitNumber,
                    Title = r.Title,
                    Status = r.Status.Name
                })
                .ToListAsync();

            return Ok(requests);
        }

        // APPLICATION STATISTICS
        [HttpGet("applications")]
        public async Task<IActionResult> GetApplicationStatistics()
        {
            var totalApplications = await _context.Applications
                .CountAsync();

            var pendingApplications = await _context.Applications
                .CountAsync(a => a.StatusId == 1);

            var approvedApplications = await _context.Applications
                .CountAsync(a => a.StatusId == 2);

            var rejectedApplications = await _context.Applications
                .CountAsync(a => a.StatusId == 3);

            return Ok(new
            {
                TotalApplications = totalApplications,
                PendingApplications = pendingApplications,
                ApprovedApplications = approvedApplications,
                RejectedApplications = rejectedApplications
            });
        }

        // OCCUPANCY STATISTICS
        [HttpGet("occupancy")]
        public async Task<IActionResult> GetOccupancyStatistics()
        {
            var totalUnits = await _context.Units
                .CountAsync();

            var occupiedUnits = await _context.Units
                .CountAsync(u => u.StatusId == 2);

            var availableUnits = await _context.Units
                .CountAsync(u => u.StatusId == 1);

            double occupiedPercentage = 0;

            if (totalUnits > 0)
            {
                occupiedPercentage =
                    ((double)occupiedUnits / totalUnits) * 100;
            }

            return Ok(new
            {
                TotalUnits = totalUnits,
                OccupiedUnits = occupiedUnits,
                AvailableUnits = availableUnits,
                OccupiedPercentage = Math.Round(occupiedPercentage, 2)
            });
        }

        [HttpGet("occupancy-by-property")]
        public async Task<IActionResult> GetOccupancyByProperty()
        {
            var properties = await _context.Properties
                .Select(p => new PropertyOccupancyDto
                {
                    PropertyName = p.Name,

                    OccupiedUnits =
                        p.Units.Count(u => u.StatusId == 2),

                    AvailableUnits =
                        p.Units.Count(u => u.StatusId == 1),

                    TotalUnits =
                        p.Units.Count(),

                    OccupancyRate =
                        p.Units.Count() == 0
                            ? 0
                            : Math.Round(
                                (double)p.Units.Count(u => u.StatusId == 2)
                                / p.Units.Count() * 100,
                                2)
                })
                .ToListAsync();

            return Ok(properties);
        }
    }
}