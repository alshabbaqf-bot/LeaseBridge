using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.Reporting.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult LeaseReports() => View();
        public IActionResult MaintenanceReports() => View();
    }
}
