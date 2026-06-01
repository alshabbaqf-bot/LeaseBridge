using LeaseBridge.Reporting.Dtos;

namespace LeaseBridge.Reporting.ViewModels
{
    public class MaintenanceReportsViewModel
    {
        public MaintenanceStatisticsDto Statistics
        { get; set; } = new();

        public List<HighPriorityRequestDto> HighPriorityRequests
        { get; set; } = new();
    }
}
