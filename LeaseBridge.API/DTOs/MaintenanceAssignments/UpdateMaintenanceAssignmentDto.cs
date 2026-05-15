using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceAssignments
{
    public class UpdateMaintenanceAssignmentDto
    {
        [Required]
        public int StaffId { get; set; }
    }
}