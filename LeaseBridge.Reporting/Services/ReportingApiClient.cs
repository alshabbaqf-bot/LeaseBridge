using LeaseBridge.Reporting.Dtos;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace LeaseBridge.Reporting.Services
{
    public class ReportingApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _contextAccessor;

        // The constructor of the ReportingApiClient class takes an HttpClient and an IHttpContextAccessor as parameters.
        // The HttpClient is used to make HTTP requests to the API,
        // while the IHttpContextAccessor allows access to the current HTTP context,
        // which is necessary for retrieving the access token for authentication.
        public ReportingApiClient(HttpClient http, IHttpContextAccessor contextAccessor)
        {
            _http = http;
            _contextAccessor = contextAccessor;
        }

        // This method is responsible for attaching the Bearer token to the outgoing HTTP request.
        // It retrieves the access token from the current HTTP context and adds it to the Authorization header of the request.
        private async Task AttachBearerTokenAsync(HttpRequestMessage request)
        {
            var context = _contextAccessor.HttpContext;
            if (context == null) return;
            var token = await context.GetTokenAsync("access_token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // This method handles the login process by sending a POST request to the API's login endpoint with the user's credentials.
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            // The method sends a POST request to the "api/auth/login" endpoint with the login request data serialized as JSON.
            var response = await _http.PostAsJsonAsync("api/auth/login", request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<OccupancyStatisticsDto?> GetOccupancyStatisticsAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/dashboard/occupancy");

            await AttachBearerTokenAsync(request);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<OccupancyStatisticsDto>();
        }

    }
}
