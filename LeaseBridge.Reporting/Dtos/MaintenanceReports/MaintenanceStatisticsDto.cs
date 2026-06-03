namespace LeaseBridge.Reporting.Dtos.MaintenanceReports
{
    public class MaintenanceStatisticsDto
    {
        public int TotalRequests { get; set; }

        public int OpenRequests { get; set; }

        public int InProgressRequests { get; set; }

        public int CompletedRequests { get; set; }

        public int HighPriorityRequests { get; set; }

        public int TotalAssignments { get; set; }
    }
}
