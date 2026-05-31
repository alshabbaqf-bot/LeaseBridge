using LeaseBridge.Reporting.Dtos;
using LeaseBridge.Reporting.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LeaseBridge.Reporting.Controllers
{
    public class AccountController : Controller
    {
        private readonly ReportingApiClient _apiClient;

        public AccountController(ReportingApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        // This method displays the login page. It accepts an optional returnUrl parameter to redirect the user back to the original page after successful login.
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // This method handles the login form submission. It validates the user's credentials by calling the API client, and if successful, it creates a claims principal and signs the user in using cookie authentication.
        public async Task<IActionResult> Login(Dtos.LoginRequest model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _apiClient.LoginAsync(model);
            if (response == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            // Parse the JWT to extract claims (no signature validation needed)
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(response.Token);

            var claims = jwtToken.Claims.ToList();
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Store the JWT in the cookie's authentication properties
            var authProperties = new AuthenticationProperties
            {
                // ExpiresUtc = response.ExpiresAt,
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
                IsPersistent = false
            };

            authProperties.StoreTokens(new[]
            {
                new AuthenticationToken { Name = "access_token", Value = response.Token }
            });

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
