using LeaseBridge.Reporting.Services;
using LeaseBridge.Reporting.ViewModels;
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
            var occupancyStats =
                await _apiClient.GetOccupancyStatisticsAsync();

            var propertyOccupancies =
                await _apiClient.GetPropertyOccupancyAsync();

            var vm = new LeaseReportsViewModel
            {
                OccupancyStatistics = occupancyStats!,
                PropertyOccupancies = propertyOccupancies ?? new()
            };

            return View(vm);
        }

        public async Task<IActionResult> InvoiceReports()
        {
            var stats =
                await _apiClient.GetPaymentStatisticsAsync();

            var overdueInvoices =
                await _apiClient.GetOverdueInvoicesAsync();

            var vm = new InvoiceReportsViewModel
            {
                Statistics = stats!,
                OverdueInvoices = overdueInvoices ?? new()
            };

            return View(vm);
        }

        public async Task<IActionResult> MaintenanceReports()
        {
            var stats =
                await _apiClient.GetMaintenanceStatisticsAsync();

            var requests =
                await _apiClient.GetHighPriorityRequestsAsync();

            var vm = new MaintenanceReportsViewModel
            {
                Statistics = stats!,
                HighPriorityRequests = requests ?? new()
            };

            return View(vm);
        }
    }
}
