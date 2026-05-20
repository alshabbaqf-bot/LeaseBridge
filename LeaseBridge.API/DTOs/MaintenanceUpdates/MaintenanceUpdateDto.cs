namespace LeaseBridge.API.DTOs.MaintenanceUpdates
{
    public class MaintenanceUpdateDto
    {
        public int UpdateId { get; set; }

        public int RequestId { get; set; }

        public int? OldStatusId { get; set; }

        public string? OldStatusName { get; set; }

        public int NewStatusId { get; set; }

        public string NewStatusName { get; set; } = null!;

        public int UpdatedBy { get; set; }

        public string UpdatedByName { get; set; } = null!;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}