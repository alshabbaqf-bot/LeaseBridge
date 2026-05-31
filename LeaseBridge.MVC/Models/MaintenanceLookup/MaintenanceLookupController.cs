using LeaseBridge.API.Data;

using LeaseBridge.MVC.Models.MaintenanceLookup;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Controllers

{

    public class MaintenanceLookupController : Controller

    {

        private readonly ApplicationDbContext _context;

        public MaintenanceLookupController(ApplicationDbContext context)

        {

            _context = context;

        }

        // GET: /MaintenanceLookup

        [HttpGet]

        public IActionResult Index()

        {

            return View(new MaintenanceLookupViewModel());

        }

        // POST: /MaintenanceLookup/CheckStatus

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CheckStatus(MaintenanceLookupViewModel model)

        {

            if (!ModelState.IsValid)

            {

                return View("Index", model);

            }

            var ticketNumber = model.TicketNumber.Trim();

            var phoneNumber = model.PhoneNumber.Trim();

            var request = await _context.MaintenanceRequests

                .Where(r => r.TicketNumber == ticketNumber)

                .Select(r => new

                {

                    r.RequestId,

                    r.TicketNumber,

                    r.Title,

                    r.Description,

                    r.CreatedAt,

                    r.TenantId,

                    r.UnitId,

                    r.CategoryId,

                    r.PriorityId,

                    r.StatusId,

                    TenantName = _context.AppUsers

                        .Where(u => u.UserId == r.TenantId)

                        .Select(u => u.FirstName + " " + u.LastName)

                        .FirstOrDefault() ?? "N/A",

                    TenantPhoneNumber = _context.AppUsers

                        .Where(u => u.UserId == r.TenantId)

                        .Select(u => u.PhoneNumber)

                        .FirstOrDefault() ?? "",

                    UnitNumber = _context.Units

                        .Where(u => u.UnitId == r.UnitId)

                        .Select(u => u.UnitNumber)

                        .FirstOrDefault() ?? "N/A",

                    PropertyName = _context.Units

                        .Where(u => u.UnitId == r.UnitId)

                        .Select(u => u.Property.Name)

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

                .FirstOrDefaultAsync();

            if (request == null)

            {

                ViewBag.ErrorMessage = "No maintenance request was found with this ticket number.";

                return View("Index", model);

            }

            if (request.TenantPhoneNumber != phoneNumber)

            {

                ViewBag.ErrorMessage = "The phone number does not match this maintenance request.";

                return View("Index", model);

            }

            var result = new PublicMaintenanceLookupResultViewModel

            {

                RequestId = request.RequestId,

                TicketNumber = request.TicketNumber,

                TenantName = request.TenantName,

                UnitNumber = request.UnitNumber,

                PropertyName = request.PropertyName,

                Title = request.Title,

                Description = request.Description,

                CategoryName = request.CategoryName,

                PriorityName = request.PriorityName,

                StatusName = request.StatusName,

                CreatedAt = request.CreatedAt

            };

            result.Updates = await _context.MaintenanceUpdates

                .Where(u => u.RequestId == request.RequestId)

                .OrderByDescending(u => u.CreatedAt)

                .Select(u => new PublicMaintenanceUpdateViewModel

                {

                    CreatedAt = u.CreatedAt,

                    Notes = u.Notes ?? "",

                    OldStatusName = _context.MaintenanceStatuses

                        .Where(s => s.StatusId == u.OldStatusId)

                        .Select(s => s.Name)

                        .FirstOrDefault() ?? "N/A",

                    NewStatusName = _context.MaintenanceStatuses

                        .Where(s => s.StatusId == u.NewStatusId)

                        .Select(s => s.Name)

                        .FirstOrDefault() ?? "N/A",

                    UpdatedByName = _context.AppUsers

                        .Where(a => a.UserId == u.UpdatedBy)

                        .Select(a => a.FirstName + " " + a.LastName)

                        .FirstOrDefault() ?? "System"

                })

                .ToListAsync();

            model.Result = result;

            return View("Index", model);

        }

    }

}
