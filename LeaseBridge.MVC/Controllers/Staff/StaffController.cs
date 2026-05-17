using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.MVC.Controllers.Staff
{
    public class StaffController : Controller
    {
        //Dashboard
        public IActionResult Dashboard() => View();
    }
}
