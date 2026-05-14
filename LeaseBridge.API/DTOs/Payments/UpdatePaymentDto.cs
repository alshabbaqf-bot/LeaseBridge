using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Payments
{
    public class UpdatePaymentDto
    {
        [Required]
        public int LeaseId { get; set; }

        [Required]
        public int MethodId { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(255)]
        public string? TransactionReference { get; set; }
    }
}