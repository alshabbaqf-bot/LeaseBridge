using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.Reporting.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Occupancy() => View();
        public IActionResult MaintenanceStatus() => View();
        public IActionResult ResolutionTime() => View();
        public IActionResult OverduePayments() => View();
    }
}
