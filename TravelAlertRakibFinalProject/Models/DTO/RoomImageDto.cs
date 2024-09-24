namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class RoomImageDto
    {

      
        public int RoomImageId { get; set; }
        public int RoomId { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
        public string ImageResolution { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

       
      
    }
}
