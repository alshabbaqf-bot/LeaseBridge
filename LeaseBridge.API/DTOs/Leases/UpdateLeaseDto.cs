using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Leases
{
    public class UpdateLeaseDto
    {
        [Required]
        public int TenantId { get; set; }

        [Required]
        public int UnitId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        public bool IsActive { get; set; }
    }
}