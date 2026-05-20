using System.ComponentModel.DataAnnotations;

namespace LeaseBridge.API.DTOs.Feedbacks
{
    public class UpdateFeedbackDto
    {
        [Required]
        [StringLength(500)]
        public string Message { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}