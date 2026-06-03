namespace LeaseBridge.API.DTOs.Reports
{
    public class ResolutionTimeByMonthDto
    {
        public string Month { get; set; } = string.Empty;

        public double AverageResolutionDays { get; set; }
    }
}
