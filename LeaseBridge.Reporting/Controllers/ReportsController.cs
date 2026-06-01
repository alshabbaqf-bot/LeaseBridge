using LeaseBridge.Reporting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.Reporting.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ReportsController : Controller
    {
        private readonly ReportingApiClient _apiClient;

        public ReportsController(ReportingApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LeaseReports()
    {
        var occupancy =
            await _apiClient.GetOccupancyStatisticsAsync();

        return View(occupancy);
    }

        public IActionResult MaintenanceReports() => View();
    }
}
