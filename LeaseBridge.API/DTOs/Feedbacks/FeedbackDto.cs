namespace LeaseBridge.API.DTOs.Feedbacks
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }

        public int TenantId { get; set; }

        public string TenantName { get; set; } = null!;

        public int? RequestId { get; set; }

        public string Message { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}