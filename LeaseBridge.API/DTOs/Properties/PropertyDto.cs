namespace LeaseBridge.API.DTOs.Properties
{
    public class PropertyDto
    {
        public int PropertyId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string? Description { get; set; }

        public int ManagerId { get; set; }
    }
}