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

        public IActionResult Index() => View();

        //public async Task<IActionResult> LeaseReports()
        //{
        //    var occupancyTask =
        //        _apiClient.GetOccupancyStatisticsAsync();

        //    var propertyTask =
        //        _apiClient.GetPropertyOccupancyAsync();

        //    await Task.WhenAll(
        //        occupancyTask,
        //        propertyTask);

        //    var model = new LeaseReportsViewModel
        //    {
        //        OccupancyStatistics =
        //            occupancyTask.Result ?? new(),

        //        PropertyOccupancies =
        //            propertyTask.Result ?? new()
        //    };

        //    return View(model);
        //}

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

        //public async Task<IActionResult> InvoiceReports()
        //{
        //    var statisticsTask =
        //        _apiClient.GetPaymentStatisticsAsync();

        //    var overdueTask =
        //        _apiClient.GetOverdueInvoicesAsync();

        //    var monthlyStatusTask =
        //        _apiClient.GetInvoiceStatusByMonthAsync();

        //    await Task.WhenAll(
        //        statisticsTask,
        //        overdueTask,
        //        monthlyStatusTask);

        //    var model = new InvoiceReportsViewModel
        //    {
        //        Statistics =
        //            statisticsTask.Result ?? new(),

        //        OverdueInvoices =
        //            overdueTask.Result ?? new(),

        //        MonthlyStatus =
        //            monthlyStatusTask.Result ?? new()
        //    };

        //    return View(model);
        //}

        public async Task<IActionResult> InvoiceReports()
        {
            var model = new InvoiceReportsViewModel
            {
                Statistics =
                    await _apiClient.GetPaymentStatisticsAsync()
                    ?? new PaymentStatisticsDto(),

                OverdueInvoices =
                    await _apiClient.GetOverdueInvoicesAsync()
                    ?? new List<OverdueInvoiceDto>(),

                MonthlyStatus =
                    await _apiClient.GetInvoiceStatusByMonthAsync()
                    ?? new List<InvoiceStatusByMonthDto>()
            };

            return View(model);
        }

        public async Task<IActionResult> MaintenanceReports()
        {
            var statisticsTask =
                _apiClient.GetMaintenanceStatisticsAsync();

            var highPriorityTask =
                _apiClient.GetHighPriorityRequestsAsync();

            var statusTask =
                _apiClient.GetMaintenanceStatusByMonthAsync();

            var resolutionTask =
                _apiClient.GetResolutionTimeByMonthAsync();

            await Task.WhenAll(
                statisticsTask,
                highPriorityTask,
                statusTask,
                resolutionTask);

            var model = new MaintenanceReportsViewModel
            {
                Statistics = statisticsTask.Result ?? new(),
                HighPriorityRequests = highPriorityTask.Result ?? new(),
                StatusByMonth = statusTask.Result ?? new(),
                ResolutionTimeByMonth = resolutionTask.Result ?? new()
            };

            return View(model);
        }
    }
}
