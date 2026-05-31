namespace LeaseBridge.Reporting.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public List<string> Roles { get; set; } = new();
    }
}
