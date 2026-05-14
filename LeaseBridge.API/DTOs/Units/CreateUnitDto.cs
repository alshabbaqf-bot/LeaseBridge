using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Units
{
    public class CreateUnitDto
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitNumber { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal RentAmount { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Range(1, 10000)]
        public decimal? Size { get; set; }
    }
}