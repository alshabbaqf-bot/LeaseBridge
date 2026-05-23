using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Payments
{
    public class UpdatePaymentDto
    {
        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int MethodId { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [StringLength(255)]
        public string? TransactionReference { get; set; }
    }
}