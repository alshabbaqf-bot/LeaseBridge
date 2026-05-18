using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.MVC.Controllers.Public
{
    public class PublicController : Controller
    {

        public IActionResult Units()
        {
            return View();
        }

        public IActionResult MaintenanceLookup()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
