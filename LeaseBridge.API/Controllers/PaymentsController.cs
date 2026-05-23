using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Payments;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _context.Payments
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    InvoiceId = p.InvoiceId,
                    MethodId = p.MethodId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    TransactionReference = p.TransactionReference,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _context.Payments
                .Where(p => p.PaymentId == id)
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    InvoiceId = p.InvoiceId,
                    MethodId = p.MethodId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    TransactionReference = p.TransactionReference,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }

        // GET: api/Payments/invoice/1
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetPaymentsByInvoice(int invoiceId)
        {
            var payments = await _context.Payments
                .Where(p => p.InvoiceId == invoiceId)
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    InvoiceId = p.InvoiceId,
                    MethodId = p.MethodId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    TransactionReference = p.TransactionReference,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            return Ok(payments);
        }

        // POST: api/Payments
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate invoice exists
            var invoiceExists = await _context.Invoices
                .AnyAsync(i => i.InvoiceId == dto.InvoiceId);

            if (!invoiceExists)
                return BadRequest("Invoice does not exist.");

            // Validate payment date
            if (dto.PaymentDate > DateTime.Now.AddDays(1))
            {
                return BadRequest("Payment date cannot be in the future.");
            }

            var payment = new Payment
            {
                InvoiceId = dto.InvoiceId,
                MethodId = dto.MethodId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                TransactionReference = dto.TransactionReference,
                CreatedAt = DateTime.Now
            };

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Payment created successfully.",
                PaymentId = payment.PaymentId
            });
        }

        // PUT: api/Payments/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(
            int id,
            UpdatePaymentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return NotFound("Payment not found.");

            // Validate invoice exists
            var invoiceExists = await _context.Invoices
                .AnyAsync(i => i.InvoiceId == dto.InvoiceId);

            if (!invoiceExists)
                return BadRequest("Invoice does not exist.");

            payment.InvoiceId = dto.InvoiceId;
            payment.MethodId = dto.MethodId;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.TransactionReference = dto.TransactionReference;
            payment.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Payment updated successfully.");
        }

        // DELETE: api/Payments/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return NotFound("Payment not found.");

            _context.Payments.Remove(payment);

            await _context.SaveChangesAsync();

            return Ok("Payment deleted successfully.");
        }
    }
}