using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Invoices
{
    public class UpdateInvoiceDto
    {
        public int? PaymentId { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}