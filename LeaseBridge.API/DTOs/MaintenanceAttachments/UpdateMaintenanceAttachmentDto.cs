using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.MaintenanceAttachments
{
    public class UpdateMaintenanceAttachmentDto
    {
        [Required]
        [Url]
        public string FileUrl { get; set; } = null!;
    }
}