using LeaseBridge.MVC.Models.Auth;

using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

using System.Text.Json;

namespace LeaseBridge.MVC.Controllers

{
    [Route("[controller]/[action]")]

    public class AccountController : Controller

    {

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public AccountController(IHttpClientFactory httpClientFactory, IConfiguration configuration)

        {

            _httpClientFactory = httpClientFactory;

            _configuration = configuration;

        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        // POST: /Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7122";
            var client = _httpClientFactory.CreateClient();

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiBaseUrl}/api/Auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View(model);
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var loginResponse = JsonSerializer.Deserialize<LoginResponseViewModel>(
                responseBody,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (loginResponse == null || string.IsNullOrWhiteSpace(loginResponse.Token))
            {
                ViewBag.ErrorMessage = "Login failed. Token was not returned by the API.";
                return View(model);
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponse.Token);

            var identityUserId =
                jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value
                ?? jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(identityUserId))
            {
                ViewBag.ErrorMessage = "Login failed. User identity was not found in the token.";
                return View(model);
            }

            var role = loginResponse.Roles.FirstOrDefault() ?? "";

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, identityUserId),
        new Claim(ClaimTypes.Email, loginResponse.Email ?? model.Email),
        new Claim(ClaimTypes.Name, loginResponse.Email ?? model.Email)
    };

            foreach (var userRole in loginResponse.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            HttpContext.Session.SetString("JwtToken", loginResponse.Token);
            HttpContext.Session.SetString("UserEmail", loginResponse.Email ?? model.Email);
            HttpContext.Session.SetString("UserRole", role);

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return LocalRedirect(model.ReturnUrl);
            }

            if (role == "Tenant")
            {
                return RedirectToAction("Index", "Home", new { area = "Tenant" });
            }

            if (role == "Property Manager")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Management" });
            }

            if (role == "Staff")
            {
                return RedirectToAction("Index", "Home", new { area = "Staff" });
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: /Account/Register

        public IActionResult Register()

        {

            return View();

        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7122";
            var client = _httpClientFactory.CreateClient();
            var registerRequest = new
            {
                model.FirstName,
                model.LastName,
                model.Email,
                model.PhoneNumber,
                model.Password,
                Role = "Tenant"
            };
            var json = JsonSerializer.Serialize(registerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync($"{apiBaseUrl}/api/Auth/register", content);
            }
            catch
            {
                ViewBag.ErrorMessage = "Registration service is not available. Please make sure the API project is running.";
                return View(model);
            }
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(error))
                {
                    ViewBag.ErrorMessage = error;
                }
                else
                {
                    ViewBag.ErrorMessage = "Registration failed. Please check your information and try again.";
                }
                return View(model);
            }
            TempData["SuccessMessage"] = "Account created successfully. Please login.";
            return RedirectToAction("Login", "Account", new { area = "" });
        }
        // POST: /Account/Logout

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: /Account/AccessDenied

        public IActionResult AccessDenied()

        {

            return View();

        }

    }

    public class LoginResponseViewModel

    {

        public string Token { get; set; } = string.Empty;

        public string? Email { get; set; }

        public List<string> Roles { get; set; } = new();

    }

}
