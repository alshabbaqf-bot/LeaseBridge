namespace LeaseBridge.API.DTOs.Applications
{
    public class ApplicationDto
    {
        public int ApplicationId { get; set; }

        public int TenantId { get; set; }

        public int UnitId { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}