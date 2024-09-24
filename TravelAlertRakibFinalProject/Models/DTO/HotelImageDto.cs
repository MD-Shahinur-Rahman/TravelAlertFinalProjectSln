namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class HotelImageDto
    {

        public int HotelId { get; set; }
        public IFormFile ImageProfile { get; set; }
        public string? ImageResolution { get; set; }
        public string? ImageUrl { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}