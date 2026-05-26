using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LeaseBridge.MVC.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    // [Authorize(Roles = "Tenant")]
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
                return View(new List<Payment>());
            }

            var payments = await _context.Payments
                .Include(p => p.Status)
                .Include(p => p.Method)
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .Where(p => p.Lease.TenantId == tenant.UserId)
                .OrderByDescending(p => p.DueDate)
                .ToListAsync();

            return View(payments);
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

            var payment = await _context.Payments
                .Include(p => p.Status)
                .Include(p => p.Method)
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .FirstOrDefaultAsync(p =>
                    p.PaymentId == id &&
                    p.Lease.TenantId == tenant.UserId);

            if (payment == null)
            {
                return NotFound();
            }

            if (payment.Status != null && payment.Status.Name == "Paid")
            {
                TempData["ErrorMessage"] = "This payment has already been paid.";
                return RedirectToAction(nameof(MyPayments));
            }

            ViewBag.PaymentMethods = await _context.PaymentMethods
                .OrderBy(m => m.Name)
                .ToListAsync();

            return View(payment);
        }

        // POST: /Tenant/Payment/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(int paymentId, int paymentMethodId)
        {
            var tenant = await GetCurrentTenantAsync();

            if (tenant == null)
            {
                TempData["ErrorMessage"] = "Tenant profile was not found for the logged-in user.";
                return RedirectToAction(nameof(MyPayments));
            }

            var payment = await _context.Payments
                .Include(p => p.Lease)
                .FirstOrDefaultAsync(p =>
                    p.PaymentId == paymentId &&
                    p.Lease.TenantId == tenant.UserId);

            if (payment == null)
            {
                return NotFound();
            }

            var methodExists = await _context.PaymentMethods
                .AnyAsync(m => m.MethodId == paymentMethodId);

            if (!methodExists)
            {
                TempData["ErrorMessage"] = "Invalid payment method selected.";
                return RedirectToAction(nameof(Pay), new { id = paymentId });
            }

            var paidStatus = await _context.PaymentStatuses
                .FirstOrDefaultAsync(s => s.Name == "Paid");

            if (paidStatus == null)
            {
                TempData["ErrorMessage"] = "Paid status was not found in the database.";
                return RedirectToAction(nameof(Pay), new { id = paymentId });
            }

            payment.PaymentDate = DateTime.UtcNow;
            payment.MethodId = paymentMethodId;
            payment.StatusId = paidStatus.StatusId;
            payment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Payment completed successfully.";
            return RedirectToAction(nameof(MyPayments));
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

            var payment = await _context.Payments
                .Include(p => p.Status)
                .Include(p => p.Method)
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Unit)
                        .ThenInclude(u => u.Property)
                .FirstOrDefaultAsync(p =>
                    p.PaymentId == id &&
                    p.Lease.TenantId == tenant.UserId);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
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