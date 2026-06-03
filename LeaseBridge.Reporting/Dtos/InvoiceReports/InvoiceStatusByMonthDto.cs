namespace LeaseBridge.Reporting.Dtos.InvoiceReports
{
    public class InvoiceStatusByMonthDto
    {
        public string Month { get; set; } = string.Empty;

        public int PaidCount { get; set; }

        public int PendingCount { get; set; }

        public int OverdueCount { get; set; }
    }
}
