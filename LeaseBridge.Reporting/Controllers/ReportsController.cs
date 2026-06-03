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
            var model = new MaintenanceReportsViewModel
            {
                Statistics =
                    await _apiClient.GetMaintenanceStatisticsAsync()
                    ?? new(),

                HighPriorityRequests =
                    await _apiClient.GetHighPriorityRequestsAsync()
                    ?? new(),

                StatusByMonth =
                    await _apiClient.GetMaintenanceStatusByMonthAsync()
                    ?? new()
            };

            return View(model);
        }
    }
}
