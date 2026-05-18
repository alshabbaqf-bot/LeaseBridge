namespace LeaseBridge.API.DTOs.UnitImages
{
    public class UnitImageDto
    {
        public int ImageId { get; set; }

        public int UnitId { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}