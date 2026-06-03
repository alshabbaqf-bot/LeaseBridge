using LeaseBridge.Reporting.Dtos.InvoiceReports;

namespace LeaseBridge.Reporting.ViewModels
{
    public class InvoiceReportsViewModel
    {
        public PaymentStatisticsDto Statistics
        { get; set; } = new();

        public List<InvoiceStatusByMonthDto> MonthlyStatus
        { get; set; } = new();

        public List<OverdueInvoiceDto> OverdueInvoices
        { get; set; } = new();
    }
}
