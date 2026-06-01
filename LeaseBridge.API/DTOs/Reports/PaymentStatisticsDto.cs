namespace LeaseBridge.API.DTOs.Reports
{
    public class PaymentStatisticsDto
    {
        public int TotalPayments { get; set; }

        public int PaidInvoices { get; set; }

        public int PendingInvoices { get; set; }

        public int OverdueInvoices { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
