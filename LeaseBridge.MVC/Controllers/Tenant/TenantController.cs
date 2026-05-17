using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.MVC.Controllers.Tenant
{
    public class TenantController : Controller
    {
        //Dashboard
        public IActionResult Dashboard() => View();
    }
}
