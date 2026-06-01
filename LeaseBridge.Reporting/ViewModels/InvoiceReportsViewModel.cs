using LeaseBridge.Reporting.DTOs;

namespace LeaseBridge.Reporting.ViewModels
{
    public class InvoiceReportsViewModel
    {
        public PaymentStatisticsDto Statistics
        { get; set; } = new();

        public List<OverdueInvoiceDto> OverdueInvoices
        { get; set; } = new();
    }
}
