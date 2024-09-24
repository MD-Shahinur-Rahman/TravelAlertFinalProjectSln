namespace TravelAlertRakibFinalProject.Models.DTO
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string HotelCode { get; set; }
        public decimal AverageRoomRate { get; set; }
        public int LocationID { get; set; } 
        public ICollection<HotelFacilityDto> HotelFacilities { get; set; } = new List<HotelFacilityDto>();
    }
}