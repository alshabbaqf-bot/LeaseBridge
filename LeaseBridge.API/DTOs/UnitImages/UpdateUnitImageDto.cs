using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.UnitImages
{
    public class UpdateUnitImageDto
    {
        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;
    }
}