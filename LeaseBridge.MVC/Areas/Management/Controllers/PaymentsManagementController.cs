using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Property Manager")]
    public class PaymentsManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Management/PaymentsManagement

        public async Task<IActionResult> Index()

        {

            await UpdateInvoiceStatusesByDueDateAsync();

            var invoices = await _context.Set<Invoice>()

                .Include(i => i.Status)

                .Include(i => i.Lease)

                    .ThenInclude(l => l.Tenant)

                .Include(i => i.Lease)

                    .ThenInclude(l => l.Unit)

                        .ThenInclude(u => u.Property)

                .Include(i => i.Payments)

                    .ThenInclude(p => p.Method)

                .OrderByDescending(i => i.DueDate)

                .ToListAsync();

            return View(invoices);

        }


        // GET: /Management/PaymentsManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Status)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .Include(i => i.Payments)
                    .ThenInclude(p => p.Method)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: /Management/PaymentsManagement/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        // POST: /Management/PaymentsManagement/Create

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(

            int leaseId,

            string invoiceNumber,

            decimal amount,

            DateTime issuedDate,

            DateTime dueDate)

        {

            if (string.IsNullOrWhiteSpace(invoiceNumber))

            {

                ModelState.AddModelError("invoiceNumber", "Invoice number is required.");

            }

            if (amount <= 0)

            {

                ModelState.AddModelError("amount", "Amount must be greater than zero.");

            }

            if (dueDate.Date < issuedDate.Date)

            {

                ModelState.AddModelError("dueDate", "Due date cannot be before the issued date.");

            }

            var lease = await _context.Leases

                .Include(l => l.Tenant)

                .Include(l => l.Unit)

                .FirstOrDefaultAsync(l => l.LeaseId == leaseId && l.IsActive);

            if (lease == null)

            {

                ModelState.AddModelError("leaseId", "Please select a valid active lease.");

            }

            var pendingStatus = await _context.Set<InvoiceStatus>()

                .FirstOrDefaultAsync(s => s.Name == "Pending");

            var overdueStatus = await _context.Set<InvoiceStatus>()

                .FirstOrDefaultAsync(s => s.Name == "Overdue");

            if (pendingStatus == null || overdueStatus == null)

            {

                ModelState.AddModelError("", "Invoice statuses Pending/Overdue were not found.");

            }

            if (!ModelState.IsValid)

            {

                await LoadDropdownsAsync();

                return View();

            }

            var today = DateTime.Today;

            var automaticStatusId = dueDate.Date < today

                ? overdueStatus!.StatusId

                : pendingStatus!.StatusId;

            var invoice = new Invoice

            {

                LeaseId = leaseId,

                InvoiceNumber = invoiceNumber.Trim(),

                Amount = amount,

                IssuedDate = issuedDate,

                DueDate = dueDate,

                StatusId = automaticStatusId

            };

            _context.Set<Invoice>().Add(invoice);

            await _context.SaveChangesAsync();

            var notification = new Notification

            {

                UserId = lease!.TenantId,

                Message = $"A new invoice {invoice.InvoiceNumber} has been created for {invoice.Amount:0.000} BHD.",

                NotificationType = "Invoice Created",

                IsRead = false,

                CreatedAt = DateTime.UtcNow

            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Invoice created successfully and tenant was notified.";

            return RedirectToAction(nameof(Index));

        }


        // GET: /Management/PaymentsManagement/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Lease)
                .Include(i => i.Status)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            await LoadDropdownsAsync(invoice);

            return View(invoice);
        }

        // POST: /Management/PaymentsManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            int leaseId,
            string invoiceNumber,
            decimal amount,
            DateTime issuedDate,
            DateTime dueDate)
        {
            var invoice = await _context.Set<Invoice>()
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                ModelState.AddModelError("invoiceNumber", "Invoice number is required.");
            }

            if (amount <= 0)
            {
                ModelState.AddModelError("amount", "Amount must be greater than zero.");
            }

            if (dueDate.Date < issuedDate.Date)
            {
                ModelState.AddModelError("dueDate", "Due date cannot be before the issued date.");
            }

            var leaseExists = await _context.Leases
                .AnyAsync(l => l.LeaseId == leaseId && l.IsActive);

            if (!leaseExists)
            {
                ModelState.AddModelError("leaseId", "Please select a valid active lease.");
            }

            var paidStatus = await _context.Set<InvoiceStatus>()
                .FirstOrDefaultAsync(s => s.Name == "Paid");

            var pendingStatus = await _context.Set<InvoiceStatus>()
                .FirstOrDefaultAsync(s => s.Name == "Pending");

            var overdueStatus = await _context.Set<InvoiceStatus>()
                .FirstOrDefaultAsync(s => s.Name == "Overdue");

            if (paidStatus == null || pendingStatus == null || overdueStatus == null)
            {
                ModelState.AddModelError("", "Invoice statuses Paid, Pending, or Overdue were not found.");
            }

            if (!ModelState.IsValid)
            {
                invoice.LeaseId = leaseId;
                invoice.InvoiceNumber = invoiceNumber;
                invoice.Amount = amount;
                invoice.IssuedDate = issuedDate;
                invoice.DueDate = dueDate;

                await LoadDropdownsAsync(invoice);
                return View(invoice);
            }

            invoice.LeaseId = leaseId;
            invoice.InvoiceNumber = invoiceNumber.Trim();
            invoice.Amount = amount;
            invoice.IssuedDate = issuedDate;
            invoice.DueDate = dueDate;

            // If invoice is already paid, keep it paid.
            // Otherwise calculate Pending/Overdue automatically.
            if (invoice.StatusId != paidStatus!.StatusId)
            {
                invoice.StatusId = dueDate.Date < DateTime.Today
                    ? overdueStatus!.StatusId
                    : pendingStatus!.StatusId;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Invoice updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Management/PaymentsManagement/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Status)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: /Management/PaymentsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int invoiceId)
        {
            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.Payments.Any())
            {
                TempData["ErrorMessage"] = "This invoice has payment transactions and cannot be deleted.";
                return RedirectToAction(nameof(Delete), new { id = invoiceId });
            }

            _context.Set<Invoice>().Remove(invoice);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Invoice deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Management/PaymentsManagement/Overdue
        public async Task<IActionResult> Overdue()
        {
            var today = DateTime.Today;

            var invoices = await _context.Set<Invoice>()
                .Include(i => i.Status)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .Include(i => i.Payments)
                    .ThenInclude(p => p.Method)
                .Where(i =>
                    i.Status.Name == "Overdue" ||
                    (i.DueDate < today && i.Status.Name != "Paid"))
                .OrderBy(i => i.DueDate)
                .ToListAsync();

            ViewBag.IsOverduePage = true;
            return View("Index", invoices);
        }

        private async Task UpdateInvoiceStatusesByDueDateAsync()

        {

            var paidStatus = await _context.Set<InvoiceStatus>()

                .FirstOrDefaultAsync(s => s.Name == "Paid");

            var pendingStatus = await _context.Set<InvoiceStatus>()

                .FirstOrDefaultAsync(s => s.Name == "Pending");

            var overdueStatus = await _context.Set<InvoiceStatus>()

                .FirstOrDefaultAsync(s => s.Name == "Overdue");

            if (paidStatus == null || pendingStatus == null || overdueStatus == null)

            {

                return;

            }

            var today = DateTime.Today;

            var invoices = await _context.Set<Invoice>()

                .Where(i => i.StatusId != paidStatus.StatusId)

                .ToListAsync();

            foreach (var invoice in invoices)

            {

                if (invoice.DueDate.Date < today)

                {

                    invoice.StatusId = overdueStatus.StatusId;

                }

                else

                {

                    invoice.StatusId = pendingStatus.StatusId;

                }

            }

            await _context.SaveChangesAsync();

        }


        private async Task LoadDropdownsAsync(Invoice? invoice = null)
        {
            var leases = await _context.Leases
                .Include(l => l.Tenant)
                .Include(l => l.Unit)
                    .ThenInclude(u => u.Property)
                .Where(l => l.IsActive || l.LeaseId == invoice!.LeaseId)
                .OrderBy(l => l.LeaseId)
                .Select(l => new
                {
                    l.LeaseId,
                    DisplayText =
                        "Lease #" + l.LeaseId +
                        " - " + l.Tenant.FirstName + " " + l.Tenant.LastName +
                        " - Unit " + l.Unit.UnitNumber +
                        " - " + l.Unit.Property.Name
                })
                .ToListAsync();

            ViewData["LeaseId"] = new SelectList(
                leases,
                "LeaseId",
                "DisplayText",
                invoice?.LeaseId
            );
            var selectedLeaseId = invoice?.LeaseId;

        }
    }
}