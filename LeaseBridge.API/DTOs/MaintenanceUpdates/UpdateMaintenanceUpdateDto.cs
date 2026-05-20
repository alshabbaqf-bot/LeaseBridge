using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceUpdates
{
    public class UpdateMaintenanceUpdateDto
    {
        [Required]
        public int NewStatusId { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}