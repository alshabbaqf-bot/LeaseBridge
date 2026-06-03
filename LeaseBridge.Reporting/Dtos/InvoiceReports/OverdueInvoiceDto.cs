namespace LeaseBridge.Reporting.Dtos.InvoiceReports
{
    public class OverdueInvoiceDto
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public string TenantName { get; set; } = string.Empty;
    }
}
