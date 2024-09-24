﻿namespace TravelAlertRakibFinalProject.Models
{
    public class HotelImage
    {
        public int HotelImageId { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public string? ImageUrl { get; set; } 
        public string? ImageResolution { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; } // Corrected typo

        // Foreign Key
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
