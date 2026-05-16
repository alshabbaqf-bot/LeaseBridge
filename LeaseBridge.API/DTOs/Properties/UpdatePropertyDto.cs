using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Properties
{
    public class UpdatePropertyDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }
}