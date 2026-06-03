using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.Reporting.Dtos.Account
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
