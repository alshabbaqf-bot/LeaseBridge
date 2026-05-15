using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Applications
{
    public class UpdateApplicationDto
    {
        [Required]
        public int StatusId { get; set; }
    }
}