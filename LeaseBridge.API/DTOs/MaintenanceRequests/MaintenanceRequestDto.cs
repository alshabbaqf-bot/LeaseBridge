namespace LeaseBridge.API.DTOs.MaintenanceRequests
{
    public class MaintenanceRequestDto
    {
        public int RequestId { get; set; }

        public int TenantId { get; set; }

        public int UnitId { get; set; }

        public int CategoryId { get; set; }

        public string TicketNumber { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int PriorityId { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}