using Microsoft.AspNetCore.Mvc;
using TravelAlertRakibFinalProject.Models;
using TravelAlertRakibFinalProject.Models.DTO;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelAlertRakibFinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomImageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RoomImageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // POST api/RoomImage
        //[HttpPost]
        //public async Task<ActionResult<RoomImage>> CreateRoomImage([FromForm] RoomImageDto roomImageDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    string uniqueFileName = null;
        //    if (roomImageDto.ImageFile != null)
        //    {
        //        string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + roomImageDto.ImageFile.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await roomImageDto.ImageFile.CopyToAsync(stream);
        //        }
        //    }

        //    RoomImage roomImage = new RoomImage
        //    {
        //        ImageUrl = uniqueFileName,
        //        ImageResolution = roomImageDto.ImageResolution,
        //        Caption = roomImageDto.Caption,
        //        IsThumbnail = roomImageDto.IsThumbnail,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOn = DateTime.UtcNow,
        //        RoomId = roomImageDto.RoomId
        //    };

        //    _context.RoomImages.Add(roomImage);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetRoomImage), new { id = roomImage.RoomImageId }, roomImage);
        //}

        [HttpPost]
        public async Task<ActionResult<RoomImage>> CreateRoomImage([FromForm] RoomImageDto roomImageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string uniqueFileName = null;
            if (roomImageDto.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + roomImageDto.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await roomImageDto.ImageFile.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
                }
            }

            RoomImage roomImage = new RoomImage
            {
                ImageUrl = uniqueFileName,
                ImageResolution = roomImageDto.ImageResolution,
                Caption = roomImageDto.Caption,
                IsThumbnail = roomImageDto.IsThumbnail,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                RoomId = roomImageDto.RoomId
            };

            _context.RoomImages.Add(roomImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoomImage), new { id = roomImage.RoomImageId }, roomImage);
        }

        // GET api/RoomImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomImage>> GetRoomImage(int id)
        {
            RoomImage roomImage = await _context.RoomImages.FindAsync(id);
            if (roomImage == null)
            {
                return NotFound();
            }
            return roomImage;
        }

        // PUT api/RoomImage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomImage(int id, [FromForm] RoomImageDto roomImageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RoomImage roomImage = await _context.RoomImages.FindAsync(id);
            if (roomImage == null)
            {
                return NotFound();
            }

            if (roomImageDto.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "Image");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + roomImageDto.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await roomImageDto.ImageFile.CopyToAsync(stream);
                }
                roomImage.ImageUrl = uniqueFileName;
            }
            //roomImage.RoomImageId = roomImageDto.RoomImageId;
            roomImage.ImageResolution = roomImageDto.ImageResolution;
            roomImage.Caption = roomImageDto.Caption;
            roomImage.IsThumbnail = roomImageDto.IsThumbnail;
            roomImage.UpdatedOn = DateTime.UtcNow;

            _context.Entry(roomImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(roomImage);
        }

        // DELETE api/RoomImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomImage(int id)
        {
            RoomImage roomImage = await _context.RoomImages.FindAsync(id);
            if (roomImage == null)
            {
                return NotFound();
            }

            _context.RoomImages.Remove(roomImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}