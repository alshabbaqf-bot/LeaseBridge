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
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tenant/Payment/MyPayments
        public async Task<IActionResult> MyPayments()
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return View(new List<Invoice>());
            }

            var invoices = await _context.Set<Invoice>()
                .Include(i => i.Status)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .Include(i => i.Payments)
                    .ThenInclude(p => p.Method)
                .Where(i => i.Lease != null && i.Lease.TenantId == tenant.UserId)
                .OrderByDescending(i => i.DueDate)
                .ToListAsync();

            return View(invoices);
        }

        // GET: /Tenant/Payment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyPayments));
            }

            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Status)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .Include(i => i.Payments)
                    .ThenInclude(p => p.Method)
                .FirstOrDefaultAsync(i =>
                    i.InvoiceId == id &&
                    i.Lease.TenantId == tenant.UserId);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: /Tenant/Payment/Pay/5
        public async Task<IActionResult> Pay(int id)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyPayments));
            }

            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Status)
                .Include(i => i.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .FirstOrDefaultAsync(i =>
                    i.InvoiceId == id &&
                    i.Lease.TenantId == tenant.UserId);

            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.Status != null &&
                invoice.Status.Name.Equals("Paid", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "This invoice has already been paid.";
                return RedirectToAction(nameof(MyPayments));
            }

            ViewBag.PaymentMethods = await _context.Set<PaymentMethod>()
                .OrderBy(m => m.Name)
                .ToListAsync();

            return View(invoice);
        }

        // POST: /Tenant/Payment/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(int invoiceId, int paymentMethodId)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyPayments));
            }

            var invoice = await _context.Set<Invoice>()
                .Include(i => i.Lease)
                .FirstOrDefaultAsync(i =>
                    i.InvoiceId == invoiceId &&
                    i.Lease.TenantId == tenant.UserId);

            if (invoice == null)
            {
                return NotFound();
            }

            var methodExists = await _context.Set<PaymentMethod>()
                .AnyAsync(m => m.MethodId == paymentMethodId);

            if (!methodExists)
            {
                TempData["ErrorMessage"] = "Invalid payment method selected.";
                return RedirectToAction(nameof(Pay), new { id = invoiceId });
            }

            var paidStatus = await _context.Set<InvoiceStatus>()
                .FirstOrDefaultAsync(s => s.Name == "Paid");

            if (paidStatus == null)
            {
                TempData["ErrorMessage"] = "Paid invoice status was not found.";
                return RedirectToAction(nameof(Pay), new { id = invoiceId });
            }

            var payment = new Payment
            {
                InvoiceId = invoice.InvoiceId,
                MethodId = paymentMethodId,
                Amount = invoice.Amount,
                PaymentDate = DateTime.UtcNow,
                TransactionReference = "TXN-" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            _context.Set<Payment>().Add(payment);

            invoice.StatusId = paidStatus.StatusId;

            await _context.SaveChangesAsync();

            var notification = new Notification
            {
                UserId = tenant.UserId,
                Message = $"Your payment for invoice {invoice.InvoiceNumber} was completed successfully.",
                NotificationType = "Payment Completed",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Payment completed successfully.";

            return RedirectToAction(
                actionName: nameof(MyPayments),
                controllerName: "Payment",
                routeValues: new { area = "Tenant" }
            );
        }

        private async Task<AppUser?> GetCurrentTenantAsync()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(identityUserId))
            {
                return null;
            }

            return await _context.Set<AppUser>()
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
    }
}