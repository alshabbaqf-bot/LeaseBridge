namespace LeaseBridge.API.DTOs.Invoices
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }

        public int LeaseId { get; set; }

        public string InvoiceNumber { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime DueDate { get; set; }

        public int StatusId { get; set; }
    }
}