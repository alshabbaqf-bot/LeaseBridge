namespace LeaseBridge.API.DTOs.Reports
{
    public class MaintenanceStatusByMonthDto
    {
        public string Month { get; set; } = string.Empty;

        public int OpenRequests { get; set; }

        public int InProgressRequests { get; set; }

        public int CompletedRequests { get; set; }
    }
}
