namespace TravelAlertRakibFinalProject.Models
{
    public class RoomFacility
    {
        public int RoomFacilityId { get; set; }

        // Foreign Keys
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int FacilityID { get; set; }
        public Facility Facility { get; set; }

        // Timestamps
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

    }
}