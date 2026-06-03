namespace LeaseBridge.Reporting.ViewModels
{
    public class OverviewDto
    {
        public int TotalProperties { get; set; }

        public int TotalUnits { get; set; }

        public int OccupiedUnits { get; set; }

        public int AvailableUnits { get; set; }

        public int ActiveLeases { get; set; }

        public int TotalTenants { get; set; }

        public int TotalStaff { get; set; }
    }
}
