using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceAssignments
{
    public class CreateMaintenanceAssignmentDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public int StaffId { get; set; }
    }
}