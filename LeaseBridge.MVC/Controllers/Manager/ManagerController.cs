using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.MVC.Controllers.Manager
{
    public class ManagerController : Controller
    {
        //Dashboard
          public IActionResult Dashboard() => View();
    }
}
