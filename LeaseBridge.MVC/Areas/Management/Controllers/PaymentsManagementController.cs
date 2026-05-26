using LeaseBridge.API.Data;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    //[Authorize(Roles = "Property Manager")]
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
            var payments = await _context.Payments
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(p => p.Method)
                .Include(p => p.Status)
                .OrderByDescending(p => p.DueDate)
                .ToListAsync();

            return View(payments);
        }

        // GET: /Management/PaymentsManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(p => p.Method)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: /Management/PaymentsManagement/Create
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        // POST: /Management/PaymentsManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("LeaseId,Amount,DueDate,PaymentDate,MethodId,StatusId")] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(payment);
                return View(payment);
            }

            if (payment.PaymentDate == default)
            {
                payment.PaymentDate = null;
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Payment record created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Management/PaymentsManagement/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            LoadDropdowns(payment);
            return View(payment);
        }

        // POST: /Management/PaymentsManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("PaymentId,LeaseId,Amount,DueDate,PaymentDate,MethodId,StatusId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                LoadDropdowns(payment);
                return View(payment);
            }

            try
            {
                if (payment.PaymentDate == default)
                {
                    payment.PaymentDate = null;
                }

                _context.Update(payment);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Payment record updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Payments.AnyAsync(p => p.PaymentId == payment.PaymentId);

                if (!exists)
                {
                    return NotFound();
                }

                throw;
            }
        }

        // GET: /Management/PaymentsManagement/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(p => p.Method)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: /Management/PaymentsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Payment record deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Management/PaymentsManagement/Overdue
        public async Task<IActionResult> Overdue()
        {
            var today = DateTime.Today;

            var payments = await _context.Payments
                .Include(p => p.Lease)
                    .ThenInclude(l => l.Tenant)
                .Include(p => p.Method)
                .Include(p => p.Status)
                .Where(p =>
                    p.Status.Name == "Overdue" ||
                    (p.DueDate < today && p.Status.Name != "Paid"))
                .OrderBy(p => p.DueDate)
                .ToListAsync();

            ViewData["Title"] = "Overdue Payments";
            ViewBag.IsOverduePage = true;

            return View("Index", payments);
        }

        private void LoadDropdowns(Payment? payment = null)
        {
            var leases = _context.Leases
                .Include(l => l.Tenant)
                .OrderBy(l => l.LeaseId)
                .Select(l => new
                {
                    l.LeaseId,
                    DisplayText = "Lease #" + l.LeaseId + " - " +
                                  l.Tenant.FirstName + " " + l.Tenant.LastName
                })
                .ToList();

            ViewData["LeaseId"] = new SelectList(
                leases,
                "LeaseId",
                "DisplayText",
                payment?.LeaseId
            );

            ViewData["MethodId"] = new SelectList(
                _context.PaymentMethods.OrderBy(m => m.Name).ToList(),
                "MethodId",
                "Name",
                payment?.MethodId
            );

            ViewData["StatusId"] = new SelectList(
                _context.PaymentStatuses.OrderBy(s => s.Name).ToList(),
                "StatusId",
                "Name",
                payment?.StatusId
            );
        }
    }
}