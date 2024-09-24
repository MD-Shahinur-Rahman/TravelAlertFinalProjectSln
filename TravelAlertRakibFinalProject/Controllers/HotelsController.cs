using Microsoft.AspNetCore.Mvc;
using TravelAlertRakibFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using TravelAlertRakibFinalProject.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace TravelAlertRakibFinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelsController(AppDbContext context)
        {
            _context = context;
        }

      
        // GET api/hotels
        [HttpGet]
        public IActionResult GetHotels()
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var hotels = connection.QueryMultiple("GetHotels", commandType: CommandType.StoredProcedure);
                var hotelList = hotels.Read<Hotel>();
                var hotelFacilityList = hotels.Read<HotelFacility>();
                var hotelImageList = hotels.Read<HotelImage>();

                var result = hotelList.Select(h => new
                {
                    Hotel = h,
                    HotelFacilities = hotelFacilityList.Where(hf => hf.HotelId == h.HotelId),
                    HotelImages = hotelImageList.Where(hi => hi.HotelId == h.HotelId)
                });

                return Ok(result);
            }
        }

        // GET api/hotels/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var hotels = connection.QueryMultiple("GetHotelById", new { id }, commandType: CommandType.StoredProcedure);
                var hotel = hotels.Read<Hotel>().FirstOrDefault();
                if (hotel == null)
                {
                    return NotFound();
                }
                var hotelFacilityList = hotels.Read<HotelFacility>();
                var hotelImageList = hotels.Read<HotelImage>();

                var result = new
                {
                    Hotel = hotel,
                    HotelFacilities = hotelFacilityList.Where(hf => hf.HotelId == hotel.HotelId),
                    HotelImages = hotelImageList.Where(hi => hi.HotelId == hotel.HotelId)
                };

                return Ok(result);
            }
        }



        [HttpPost]
        public async Task<ActionResult<HotelDto>> CreateHotel([FromBody] HotelDto hotelDto)
        {
            var location = await _context.Locations.FindAsync(hotelDto.LocationID);
            if (location == null)
            {
                return BadRequest("Invalid LocationID. Location does not exist.");
            }

            var hotel = new Hotel
            {
                HotelName = hotelDto.HotelName,
                Description = hotelDto.Description,
                StarRating = hotelDto.StarRating,
                Address = hotelDto.Address,
                ContactInfo = hotelDto.ContactInfo,
                HotelCode = hotelDto.HotelCode,
                AverageRoomRate = hotelDto.AverageRoomRate,
                LocationID = hotelDto.LocationID
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            foreach (var facilityDto in hotelDto.HotelFacilities)
            {
                var hotelFacility = new HotelFacility
                {
                    HotelId = hotel.HotelId,
                    FacilityID = facilityDto.FacilityID,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };
                hotel.HotelFacilities.Add(hotelFacility);
            }

            await _context.SaveChangesAsync();
            return Ok(hotel.HotelId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDto>> UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
            var location = await _context.Locations.FindAsync(hotelDto.LocationID);
            if (location == null)
            {
                return BadRequest("Invalid LocationID. Location does not exist.");
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            hotel.HotelName = hotelDto.HotelName;
            hotel.Description = hotelDto.Description;
            hotel.StarRating = hotelDto.StarRating;
            hotel.Address = hotelDto.Address;
            hotel.ContactInfo = hotelDto.ContactInfo;
            hotel.HotelCode = hotelDto.HotelCode;
            hotel.AverageRoomRate = hotelDto.AverageRoomRate;
            hotel.LocationID = hotelDto.LocationID;

            foreach (var facilityDto in hotelDto.HotelFacilities)
            {
                var hotelFacility = hotel.HotelFacilities.FirstOrDefault(f => f.HotelFacilityId == facilityDto.HotelFacilityId);
                if (hotelFacility != null)
                {
                    hotelFacility.FacilityID = facilityDto.FacilityID;
                    hotelFacility.CreatedOn = facilityDto.CreatedOn;
                    hotelFacility.UpdatedOn = facilityDto.UpdatedOn;
                }
                else
                {
                    hotelFacility = new HotelFacility
                    {
                        HotelId = hotel.HotelId,
                        FacilityID = facilityDto.FacilityID,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    hotel.HotelFacilities.Add(hotelFacility);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(hotelDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            _context.Hotels.Remove (hotel);
            await _context.SaveChangesAsync();
            return Ok("Hotel deleted successfully.");
        }

      
    }
}