using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlertRakibFinalProject.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string HotelCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageRoomRate { get; set; }

        public int LocationID { get; set; }
        public Location? Location { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<HotelFacility> HotelFacilities { get; set; } = new List<HotelFacility>();
        public ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();
    }
}