namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class RoomFacilityDto
    {
        public int RoomFacilityId { get; set; } // Optional for updates
        public int RoomId { get; set; } // Foreign key to Room
        public int FacilityID { get; set; } // The ID of the facility (FK)
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}