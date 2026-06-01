using System.ComponentModel.DataAnnotations;
namespace LeaseBridge.MVC.Models.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}