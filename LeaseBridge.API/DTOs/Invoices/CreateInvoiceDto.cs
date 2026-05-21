using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        [Required]
        public int LeaseId { get; set; }

        public int? PaymentId { get; set; }

        [Required]
        public string InvoiceNumber { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}