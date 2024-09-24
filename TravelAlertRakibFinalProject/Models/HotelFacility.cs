namespace TravelAlertRakibFinalProject.Models
{
    public class HotelFacility
    {
        public int HotelFacilityId { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int FacilityID { get; set; }
        public Facility Facility { get; set; }

     
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

    }
}