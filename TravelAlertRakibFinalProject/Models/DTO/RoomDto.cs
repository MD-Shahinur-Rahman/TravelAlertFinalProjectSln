namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class RoomDto
    {
        public int RoomId { get; set; } // Optional for updates
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int BedInRoom { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public string RoomStatus { get; set; } // Consider using enum for predefined statuses

        // Foreign Key
        public int HotelId { get; set; }

        // Relationships
        public ICollection<RoomFacilityDto> RoomFacilities { get; set; } = new List<RoomFacilityDto>();
    }
}

