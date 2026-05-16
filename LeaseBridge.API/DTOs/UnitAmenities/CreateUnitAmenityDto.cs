using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.UnitAmenities
{
    public class CreateUnitAmenityDto
    {
        [Required]
        public int UnitId { get; set; }

        [Required]
        public int AmenityId { get; set; }
    }
}