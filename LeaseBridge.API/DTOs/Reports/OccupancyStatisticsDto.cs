namespace LeaseBridge.API.DTOs.Reports
{
    public class OccupancyStatisticsDto
    {
        public int TotalUnits { get; set; }
        public int OccupiedUnits { get; set; }
        public int AvailableUnits { get; set; }
        public double OccupiedPercentage { get; set; }
    }
}
