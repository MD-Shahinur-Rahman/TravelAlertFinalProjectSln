using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAlertRakibFinalProject.Models.DTO;
using TravelAlertRakibFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace TravelAlertRakibFinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/rooms
        [HttpGet]
        public IActionResult GetRooms()
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var rooms = connection.QueryMultiple("GetRooms", commandType: CommandType.StoredProcedure);
                var roomList = rooms.Read<Room>();
                var roomFacilityList = rooms.Read<RoomFacility>();
                var roomImageList = rooms.Read<RoomImage>();

                var result = roomList.Select(r => new
                {
                    Room = r,
                    RoomFacilities = roomFacilityList.Where(rf => rf.RoomId == r.RoomId),
                    RoomImages = roomImageList.Where(ri => ri.RoomId == r.RoomId)
                });

                return Ok(result);
            }
        }

        // GET api/rooms/5
        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var rooms = connection.QueryMultiple("GetRoomById", new { id }, commandType: CommandType.StoredProcedure);
                var room = rooms.Read<Room>().FirstOrDefault();
                if (room == null)
                {
                    return NotFound();
                }
                var roomFacilityList = rooms.Read<RoomFacility>();
                var roomImageList = rooms.Read<RoomImage>();

                var result = new
                {
                    Room = room,
                    RoomFacilities = roomFacilityList.Where(rf => rf.RoomId == room.RoomId),
                    RoomImages = roomImageList.Where(ri => ri.RoomId == room.RoomId)
                };

                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] RoomDto roomDto)
        {
            var hotel = await _context.Hotels.FindAsync(roomDto.HotelId);
            if (hotel == null)
            {
                return BadRequest("Invalid HotelId. Hotel does not exist.");
            }

            var room = new Room
            {
                RoomType = roomDto.RoomType,
                PricePerNight = roomDto.PricePerNight,
                BedInRoom = roomDto.BedInRoom,
                RoomNumber = roomDto.RoomNumber,
                FloorNumber = roomDto.FloorNumber,
                MaxOccupancy = roomDto.MaxOccupancy,
                RoomStatus = roomDto.RoomStatus,
                HotelId = roomDto.HotelId
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            if (roomDto.RoomFacilities != null)
            {
                foreach (var facilityDto in roomDto.RoomFacilities)
                {
                    var roomFacility = new RoomFacility
                    {
                        RoomId = room.RoomId,
                        FacilityID = facilityDto.FacilityID,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    room.RoomFacilities.Add(roomFacility);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(room.RoomId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDto>> UpdateRoom(int id, [FromBody] RoomDto roomDto)
        {
            var hotel = await _context.Hotels.FindAsync(roomDto.HotelId);
            if (hotel == null)
            {
                return BadRequest("Invalid HotelId. Hotel does not exist.");
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            room.RoomType = roomDto.RoomType;
            room.PricePerNight = roomDto.PricePerNight;
            room.BedInRoom = roomDto.BedInRoom;
            room.RoomNumber = roomDto.RoomNumber;
            room.FloorNumber = roomDto.FloorNumber;
            room.MaxOccupancy = roomDto.MaxOccupancy;
            room.RoomStatus = roomDto.RoomStatus;
            room.HotelId = roomDto.HotelId;

            foreach (var facilityDto in roomDto.RoomFacilities)
            {
                var facility = await _context.Facilities.FindAsync(facilityDto.FacilityID);
                if (facility == null)
                {
                    return BadRequest($"Invalid FacilityID {facilityDto.FacilityID}. Facility does not exist.");
                }

                var roomFacility = room.RoomFacilities.FirstOrDefault(f => f.RoomFacilityId == facilityDto.RoomFacilityId);
                if (roomFacility != null)
                {
                    roomFacility.FacilityID = facilityDto.FacilityID;
                    roomFacility.CreatedOn = facilityDto.CreatedOn;
                    roomFacility.UpdatedOn = facilityDto.UpdatedOn;
                }
                else
                {
                    roomFacility = new RoomFacility
                    {
                        RoomId = room.RoomId,
                        FacilityID = facilityDto.FacilityID,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    room.RoomFacilities.Add(roomFacility);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(roomDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return Ok("Room deleted successfully.");
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<RoomDto>> GetRoomById(int id)
        //{
        //    var room = await _context.Rooms
        //        .Include(r => r.RoomFacilities)
        //        .FirstOrDefaultAsync(r => r.RoomId == id);

        //    if (room == null)
        //    {
        //        return NotFound("Room not found.");
        //    }

        //    var roomDto = new RoomDto
        //    {
        //        RoomId = room.RoomId,
        //        RoomType = room.RoomType,
        //        PricePerNight = room.PricePerNight,
        //        BedInRoom = room.BedInRoom,
        //        RoomNumber = room.RoomNumber,
        //        FloorNumber = room.FloorNumber,
        //        MaxOccupancy = room.MaxOccupancy,
        //        RoomStatus = room.RoomStatus,
        //        HotelId = room.HotelId,
        //        RoomFacilities = room.RoomFacilities.Select(f => new RoomFacilityDto
        //        {
        //            RoomFacilityId = f.RoomFacilityId,
        //            RoomId = f.RoomId,
        //            FacilityID = f.FacilityID,
        //            CreatedOn = f.CreatedOn,
        //            UpdatedOn = f.UpdatedOn
        //        }).ToList()
        //    };

        //    return Ok(roomDto);
        //}
    }
}


