using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlertRakibFinalProject.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }
        public int BedInRoom { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public string RoomStatus { get; set; } 

    
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }


        public ICollection<RoomFacility> RoomFacilities { get; set; } = new List<RoomFacility>();
        public ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
    }
}