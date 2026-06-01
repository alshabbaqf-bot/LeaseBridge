namespace LeaseBridge.Reporting.Dtos
{
    public class HighPriorityRequestDto
    {
        public int RequestId { get; set; }

        public string UnitNumber { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
