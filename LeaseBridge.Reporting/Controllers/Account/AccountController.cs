using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.Reporting.Controllers.Account
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            ViewBag.Error = "Invalid login attempt. Please check your email and password.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // 3. Redirect to the Login page
            // Assuming your Login action is in the same Account controller
            return RedirectToAction("Login", "Account");
        }
    }
}


