using LeaseBridge.Reporting.Dtos;
using LeaseBridge.Reporting.DTOs;

namespace LeaseBridge.Reporting.ViewModels
{
    public class LeaseReportsViewModel
    {
        public OccupancyStatisticsDto OccupancyStatistics
        { get; set; } = new();

        public List<PropertyOccupancyDto> PropertyOccupancies
        { get; set; } = new();
    }
}