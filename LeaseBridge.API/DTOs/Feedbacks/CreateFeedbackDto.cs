using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Feedbacks
{
    public class CreateFeedbackDto
    {
        [Required]
        public int TenantId { get; set; }

        public int? RequestId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}