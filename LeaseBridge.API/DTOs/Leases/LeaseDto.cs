namespace LeaseBridge.API.DTOs.Leases
{
    public class LeaseDto
    {
        public int LeaseId { get; set; }

        public int TenantId { get; set; }

        public int UnitId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        public bool IsActive { get; set; }
    }
}