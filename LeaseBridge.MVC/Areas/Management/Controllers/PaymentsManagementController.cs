using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Authorization;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    // [Authorize(Roles = "Property Manager")]
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
            DateTime dueDate,
            int statusId)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                ModelState.AddModelError("invoiceNumber", "Invoice number is required.");
            }

            if (amount <= 0)
            {
                ModelState.AddModelError("amount", "Amount must be greater than zero.");
            }

            var leaseExists = await _context.Leases.AnyAsync(l => l.LeaseId == leaseId);
            if (!leaseExists)
            {
                ModelState.AddModelError("leaseId", "Please select a valid lease.");
            }

            var statusExists = await _context.Set<InvoiceStatus>().AnyAsync(s => s.StatusId == statusId);
            if (!statusExists)
            {
                ModelState.AddModelError("statusId", "Please select a valid invoice status.");
            }

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View();
            }

            var invoice = new Invoice
            {
                LeaseId = leaseId,
                InvoiceNumber = invoiceNumber.Trim(),
                Amount = amount,
                IssuedDate = issuedDate,
                DueDate = dueDate,
                StatusId = statusId
            };

            _context.Set<Invoice>().Add(invoice);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Invoice created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Management/PaymentsManagement/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _context.Set<Invoice>()
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
            DateTime dueDate,
            int statusId)
        {
            var invoice = await _context.Set<Invoice>().FirstOrDefaultAsync(i => i.InvoiceId == id);

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

            var leaseExists = await _context.Leases.AnyAsync(l => l.LeaseId == leaseId);
            if (!leaseExists)
            {
                ModelState.AddModelError("leaseId", "Please select a valid lease.");
            }

            var statusExists = await _context.Set<InvoiceStatus>().AnyAsync(s => s.StatusId == statusId);
            if (!statusExists)
            {
                ModelState.AddModelError("statusId", "Please select a valid invoice status.");
            }

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(invoice);
                return View(invoice);
            }

            invoice.LeaseId = leaseId;
            invoice.InvoiceNumber = invoiceNumber.Trim();
            invoice.Amount = amount;
            invoice.IssuedDate = issuedDate;
            invoice.DueDate = dueDate;
            invoice.StatusId = statusId;

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

        private async Task LoadDropdownsAsync(Invoice? invoice = null)
        {
            var leases = await _context.Leases
                .Include(l => l.Tenant)
                .OrderBy(l => l.LeaseId)
                .Select(l => new
                {
                    l.LeaseId,
                    DisplayText = "Lease #" + l.LeaseId + " - " +
                                  l.Tenant.FirstName + " " + l.Tenant.LastName
                })
                .ToListAsync();

            ViewData["LeaseId"] = new SelectList(
                leases,
                "LeaseId",
                "DisplayText",
                invoice?.LeaseId
            );

            ViewData["StatusId"] = new SelectList(
                await _context.Set<InvoiceStatus>().OrderBy(s => s.Name).ToListAsync(),
                "StatusId",
                "Name",
                invoice?.StatusId
            );
        }
    }
}