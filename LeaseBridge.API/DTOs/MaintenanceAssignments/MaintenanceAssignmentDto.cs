namespace LeaseBridge.API.DTOs.MaintenanceAssignments
{
    public class MaintenanceAssignmentDto
    {
        public int AssignmentId { get; set; }

        public int RequestId { get; set; }

        public int StaffId { get; set; }

        public DateTime AssignedDate { get; set; }
    }
}