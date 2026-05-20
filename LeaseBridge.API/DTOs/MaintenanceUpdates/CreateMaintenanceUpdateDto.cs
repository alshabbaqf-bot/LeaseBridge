using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceUpdates
{
    public class CreateMaintenanceUpdateDto
    {
        [Required]
        public int RequestId { get; set; }

        public int? OldStatusId { get; set; }

        [Required]
        public int NewStatusId { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}