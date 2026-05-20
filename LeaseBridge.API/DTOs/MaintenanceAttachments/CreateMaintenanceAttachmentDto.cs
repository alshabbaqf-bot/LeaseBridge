using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceAttachments
{
    public class CreateMaintenanceAttachmentDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        [Url]
        public string FileUrl { get; set; } = null!;
    }
}