using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Applications
{
    public class CreateApplicationDto
    {
        [Required]
        public int TenantId { get; set; }

        [Required]
        public int UnitId { get; set; }
    }
}