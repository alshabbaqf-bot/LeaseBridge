using LeaseBridge.Reporting.Dtos.InvoiceReports;
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

        public async Task<IActionResult> LeaseReports()
        {
            var occupancyTask =
                _apiClient.GetOccupancyStatisticsAsync();

            var propertyTask =
                _apiClient.GetPropertyOccupancyAsync();

            await Task.WhenAll(
                occupancyTask,
                propertyTask);

            var model = new LeaseReportsViewModel
            {
                OccupancyStatistics =
                    occupancyTask.Result ?? new(),

                PropertyOccupancies =
                    propertyTask.Result ?? new()
            };

            return View(model);
        }

        public async Task<IActionResult> InvoiceReports()
        {
            var statisticsTask =
                _apiClient.GetPaymentStatisticsAsync();

            var overdueTask =
                _apiClient.GetOverdueInvoicesAsync();

            var monthlyStatusTask =
                _apiClient.GetInvoiceStatusByMonthAsync();

            await Task.WhenAll(
                statisticsTask,
                overdueTask,
                monthlyStatusTask);

            var model = new InvoiceReportsViewModel
            {
                Statistics =
                    statisticsTask.Result ?? new(),

                OverdueInvoices =
                    overdueTask.Result ?? new(),

                MonthlyStatus =
                    monthlyStatusTask.Result ?? new()
            };

            return View(model);
        }

        public async Task<IActionResult> MaintenanceReports()
        {
            var statisticsTask =
                _apiClient.GetMaintenanceStatisticsAsync();

            var highPriorityTask =
                _apiClient.GetHighPriorityRequestsAsync();

            var statusByMonthTask =
                _apiClient.GetMaintenanceStatusByMonthAsync();

            var resolutionByMonthTask =
                _apiClient.GetResolutionTimeByMonthAsync();

            var avgResolutionTask =
                _apiClient.GetAverageResolutionTimeAsync();

            await Task.WhenAll(
                statisticsTask,
                highPriorityTask,
                statusByMonthTask,
                resolutionByMonthTask,
                avgResolutionTask);

            var model = new MaintenanceReportsViewModel
            {
                Statistics =
                    statisticsTask.Result ?? new(),

                HighPriorityRequests =
                    highPriorityTask.Result ?? new(),

                StatusByMonth =
                    statusByMonthTask.Result ?? new(),

                ResolutionTimeByMonth =
                    resolutionByMonthTask.Result ?? new(),

                AverageResolutionTime =
                    avgResolutionTask.Result
            };

            return View(model);
        }
    }
}
