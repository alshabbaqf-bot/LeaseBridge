using LeaseBridge.Reporting.Dtos.MaintenanceReports;
using LeaseBridge.Reporting.Dtos.InvoiceReports;
using LeaseBridge.Reporting.Dtos.LeaseReports;

namespace LeaseBridge.Reporting.ViewModels
{
    public class DashboardViewModel
    {
        public OverviewDto Overview { get; set; } = new();

        public OccupancyStatisticsDto Occupancy { get; set; } = new();

        public PaymentStatisticsDto Payments { get; set; } = new();

        public MaintenanceStatisticsDto Maintenance
        { get; set; } = new();

        public List<InvoiceStatusByMonthDto> InvoiceStatusByMonth
        { get; set; } = new();

        public List<MaintenanceStatusByMonthDto> MaintenanceStatusByMonth
        { get; set; } = new();
    }
}
