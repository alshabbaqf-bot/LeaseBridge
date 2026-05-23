using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        [Required]
        public int LeaseId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } = null!;

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}