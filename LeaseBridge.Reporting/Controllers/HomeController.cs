using LeaseBridge.Reporting.Models;
using LeaseBridge.Reporting.Services;
using LeaseBridge.Reporting.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeaseBridge.Reporting.Controllers
{
    [Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ReportingApiClient _apiClient;

        public HomeController(ILogger<HomeController> logger, ReportingApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            var overviewTask =
                _apiClient.GetOverviewAsync();

            var occupancyTask =
                _apiClient.GetOccupancyStatisticsAsync();

            var paymentTask =
                _apiClient.GetPaymentStatisticsAsync();

            var maintenanceTask =
                _apiClient.GetMaintenanceStatisticsAsync();

            var invoiceMonthlyTask =
                _apiClient.GetInvoiceStatusByMonthAsync();

            var maintenanceMonthlyTask =
                _apiClient.GetMaintenanceStatusByMonthAsync();

            await Task.WhenAll(
                overviewTask,
                occupancyTask,
                paymentTask,
                maintenanceTask,
                invoiceMonthlyTask,
                maintenanceMonthlyTask);

            var vm = new DashboardViewModel
            {
                Overview =
                    overviewTask.Result ?? new(),

                Occupancy =
                    occupancyTask.Result ?? new(),

                Payments =
                    paymentTask.Result ?? new(),

                Maintenance =
                    maintenanceTask.Result ?? new(),

                InvoiceStatusByMonth =
                    invoiceMonthlyTask.Result ?? new(),

                MaintenanceStatusByMonth =
                    maintenanceMonthlyTask.Result ?? new()
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
