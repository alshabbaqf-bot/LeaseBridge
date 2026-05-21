using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Invoices;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _context.Invoices
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    LeaseId = i.LeaseId,
                    PaymentId = i.PaymentId,
                    InvoiceNumber = i.InvoiceNumber,
                    Amount = i.Amount,
                    IssuedDate = i.IssuedDate,
                    DueDate = i.DueDate,
                    IsPaid = i.IsPaid
                })
                .ToListAsync();

            return Ok(invoices);
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await _context.Invoices
                .Where(i => i.InvoiceId == id)
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    LeaseId = i.LeaseId,
                    PaymentId = i.PaymentId,
                    InvoiceNumber = i.InvoiceNumber,
                    Amount = i.Amount,
                    IssuedDate = i.IssuedDate,
                    DueDate = i.DueDate,
                    IsPaid = i.IsPaid
                })
                .FirstOrDefaultAsync();

            if (invoice == null)
                return NotFound("Invoice not found.");

            return Ok(invoice);
        }

        // POST: api/Invoices
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateInvoice(
            CreateInvoiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var leaseExists = await _context.Leases
                .AnyAsync(l => l.LeaseId == dto.LeaseId);

            if (!leaseExists)
                return BadRequest("Lease not found.");

            if (dto.PaymentId.HasValue)
            {
                var paymentExists = await _context.Payments
                    .AnyAsync(p => p.PaymentId == dto.PaymentId);

                if (!paymentExists)
                    return BadRequest("Payment not found.");
            }

            var invoice = new Invoice
            {
                LeaseId = dto.LeaseId,
                PaymentId = dto.PaymentId,
                InvoiceNumber = dto.InvoiceNumber,
                Amount = dto.Amount,
                IssuedDate = DateTime.Now,
                DueDate = dto.DueDate,
                IsPaid = false
            };

            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return Ok("Invoice created successfully.");
        }

        // PUT: api/Invoices/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(
            int id,
            UpdateInvoiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var invoice = await _context.Invoices
                .FindAsync(id);

            if (invoice == null)
                return NotFound("Invoice not found.");

            if (dto.PaymentId.HasValue)
            {
                var paymentExists = await _context.Payments
                    .AnyAsync(p => p.PaymentId == dto.PaymentId);

                if (!paymentExists)
                    return BadRequest("Payment not found.");
            }

            invoice.PaymentId = dto.PaymentId;
            invoice.IsPaid = dto.IsPaid;

            await _context.SaveChangesAsync();

            return Ok("Invoice updated successfully.");
        }

        // DELETE: api/Invoices/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices
                .FindAsync(id);

            if (invoice == null)
                return NotFound("Invoice not found.");

            _context.Invoices.Remove(invoice);

            await _context.SaveChangesAsync();

            return Ok("Invoice deleted successfully.");
        }
    }
}