using Microsoft.AspNetCore.Mvc;

namespace LeaseBridge.MVC.Controllers.Account
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
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
        public async Task<IActionResult> Logout()
        {
            // If you are using Cookie Authentication, clear the cookie scheme context here:
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}