using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Payments
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }

        public int InvoiceId { get; set; }

        public int MethodId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? TransactionReference { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}