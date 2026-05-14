using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceRequests
{
    public class CreateMaintenanceRequestDto
    {
        [Required]
        public int TenantId { get; set; }

        [Required]
        public int UnitId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        public int PriorityId { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}