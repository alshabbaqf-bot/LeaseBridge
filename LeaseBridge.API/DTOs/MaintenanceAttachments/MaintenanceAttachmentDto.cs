namespace LeaseBridge.API.DTOs.MaintenanceAttachments
{
    public class MaintenanceAttachmentDto
    {
        public int AttachmentId { get; set; }

        public int RequestId { get; set; }

        public string FileUrl { get; set; } = null!;
    }
}