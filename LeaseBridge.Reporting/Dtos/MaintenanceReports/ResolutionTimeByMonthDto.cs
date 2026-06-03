namespace LeaseBridge.Reporting.Dtos.MaintenanceReports
{
    public class ResolutionTimeByMonthDto
    {
        public string Month { get; set; } = string.Empty;

        public double AverageResolutionDays { get; set; }
    }
}
