namespace LeaseBridge.API.DTOs.Reports
{
    public class PropertyOccupancyDto
    {
        public string PropertyName { get; set; } = string.Empty;

        public int OccupiedUnits { get; set; }

        public int AvailableUnits { get; set; }

        public int TotalUnits { get; set; }

        public double OccupancyRate { get; set; }
    }
}
