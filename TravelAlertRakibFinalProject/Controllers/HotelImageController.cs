using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TravelAlertRakibFinalProject.Models;
using TravelAlertRakibFinalProject.Models.DTO;

namespace TravelAlertRakibFinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelImageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HotelImageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<HotelImageDto>> CreateHotelImage([FromForm] HotelImageDto hotelImageDto)
        {
            var hotel = await _context.Hotels.FindAsync(hotelImageDto.HotelId);
            if (hotel == null)
            {
                return BadRequest("Invalid HotelId. Hotel does not exist.");
            }

            var hotelImage = new HotelImage
            {
                HotelId = hotelImageDto.HotelId,
                Caption = hotelImageDto.Caption,
                IsThumbnail = hotelImageDto.IsThumbnail,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            if (hotelImageDto.ImageProfile != null)
            {
                using (var stream = new MemoryStream())
                {
                    await hotelImageDto.ImageProfile.CopyToAsync(stream);
                    var fileBytes = stream.ToArray();
                    var formFile = new FormFile(stream, 0, fileBytes.Length, "ImageProfile", "image.jpg");
                    var uploadedFileName = UploadFile(formFile);
                    hotelImage.ImageUrl = uploadedFileName;
                }
            }

            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();
            return Ok(hotelImage.HotelImageId);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelImageDto>> UpdateHotelImage(int id, [FromForm] HotelImageDto hotelImageDto)
        {
            var hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound("HotelImage not found.");
            }

            hotelImage.Caption = hotelImageDto.Caption;
            hotelImage.IsThumbnail = hotelImageDto.IsThumbnail;
            hotelImage.UpdatedOn = DateTime.UtcNow;

            if (hotelImageDto.ImageProfile != null)
            {
                using (var stream = new MemoryStream())
                {
                    await hotelImageDto.ImageProfile.CopyToAsync(stream);
                    var fileBytes = stream.ToArray();
                    var formFile = new FormFile(stream, 0, fileBytes.Length, "ImageProfile", "image.jpg");
                    var uploadedFileName = UploadFile(formFile);
                    hotelImage.ImageUrl = uploadedFileName;
                }
            }

            await _context.SaveChangesAsync();
            return Ok(hotelImageDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotelImage(int id)
        {
            var hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound("HotelImage not found.");
            }

            _context.HotelImages.Remove(hotelImage);
            await _context.SaveChangesAsync();
            return Ok("HotelImage deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelImageDto>> GetHotelImageById(int id)
        {
            var hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound("HotelImage not found.");
            }

            var hotelImageDto = new HotelImageDto
            {
              
                HotelId = hotelImage.HotelId,
                ImageUrl = hotelImage.ImageUrl,
                Caption = hotelImage.Caption,
                IsThumbnail = hotelImage.IsThumbnail,
                CreatedOn = hotelImage.CreatedOn,
                UpdatedOn = hotelImage.UpdatedOn
            };

            return Ok(hotelImageDto);
        }

        private string UploadFile(IFormFile file)
        {
            if (file == null) return null;
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string uploadsFolder = Path.Combine(_env.WebRootPath, "Image");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}