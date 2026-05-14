namespace LeaseBridge.API.DTOs.Units
{
    public class UnitDto
    {
        public int UnitId { get; set; }

        public int PropertyId { get; set; }

        public string UnitNumber { get; set; }

        public int TypeId { get; set; }

        public decimal RentAmount { get; set; }

        public int StatusId { get; set; }

        public decimal? Size { get; set; }
    }
}