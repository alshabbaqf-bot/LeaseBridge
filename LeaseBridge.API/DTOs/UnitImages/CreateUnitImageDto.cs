using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.UnitImages
{
    public class CreateUnitImageDto
    {
        [Required]
        public int UnitId { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;
    }
}