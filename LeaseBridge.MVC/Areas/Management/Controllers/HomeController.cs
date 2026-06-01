using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LeaseBridge.MVC.Areas.Management.Controllers
{
    [Area("Management")]
    //[Authorize(Roles = "Property Manager")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Management" });
        }
    }
}