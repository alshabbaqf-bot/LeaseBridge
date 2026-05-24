using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Invoices
{
    public class UpdateInvoiceDto
    {
        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}