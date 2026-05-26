using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LeaseBridge.MVC.Controllers
{
    public class TrackerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TrackerController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: /Tracker
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Tracker/CheckMaintenanceStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckMaintenanceStatus(string ticketNumber, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(ticketNumber) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                ViewBag.ErrorMessage = "Please enter both ticket number and registered phone number.";
                return View("Index");
            }

            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7122";

            var apiUrl =
                $"{apiBaseUrl}/api/public/maintenance-lookup?ticketNumber={Uri.EscapeDataString(ticketNumber.Trim())}&phoneNumber={Uri.EscapeDataString(phoneNumber.Trim())}";

            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "No maintenance request was found using the provided ticket number and phone number.";
                    return View("Index");
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<MaintenanceLookupResultViewModel>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (result == null)
                {
                    ViewBag.ErrorMessage = "The maintenance lookup response could not be read.";
                    return View("Index");
                }

                return View("Result", result);
            }
            catch
            {
                ViewBag.ErrorMessage = "Could not reach the maintenance lookup service right now.";
                return View("Index");
            }
        }
    }

    public class MaintenanceLookupResultViewModel
    {
        public string TicketNumber { get; set; } = string.Empty;

        public string TenantName { get; set; } = string.Empty;

        public string UnitNumber { get; set; } = string.Empty;

        public string PropertyName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string PriorityName { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}