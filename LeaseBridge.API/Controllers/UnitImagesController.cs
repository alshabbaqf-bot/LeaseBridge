using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.UnitImages;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitImages
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _context.UnitImages
                .Select(ui => new UnitImageDto
                {
                    ImageId = ui.ImageId,
                    UnitId = ui.UnitId,
                    ImageUrl = ui.ImageUrl
                })
                .ToListAsync();

            return Ok(images);
        }

        // GET: api/UnitImages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await _context.UnitImages
                .Where(ui => ui.ImageId == id)
                .Select(ui => new UnitImageDto
                {
                    ImageId = ui.ImageId,
                    UnitId = ui.UnitId,
                    ImageUrl = ui.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (image == null)
                return NotFound("Image not found.");

            return Ok(image);
        }

        // GET: api/UnitImages/unit/3
        [HttpGet("unit/{unitId}")]
        public async Task<IActionResult> GetImagesByUnit(int unitId)
        {
            var unitExists = await _context.Units
                .AnyAsync(u => u.UnitId == unitId);

            if (!unitExists)
                return NotFound("Unit not found.");

            var images = await _context.UnitImages
                .Where(ui => ui.UnitId == unitId)
                .Select(ui => new UnitImageDto
                {
                    ImageId = ui.ImageId,
                    UnitId = ui.UnitId,
                    ImageUrl = ui.ImageUrl
                })
                .ToListAsync();

            return Ok(images);
        }

        // POST: api/UnitImages
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateImage(
            CreateUnitImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var unitExists = await _context.Units
                .AnyAsync(u => u.UnitId == dto.UnitId);

            if (!unitExists)
                return BadRequest("Unit does not exist.");

            var unitImage = new UnitImage
            {
                UnitId = dto.UnitId,
                ImageUrl = dto.ImageUrl
            };

            _context.UnitImages.Add(unitImage);

            await _context.SaveChangesAsync();

            return Ok("Unit image added successfully.");
        }

        // PUT: api/UnitImages/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(
            int id,
            UpdateUnitImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = await _context.UnitImages
                .FindAsync(id);

            if (image == null)
                return NotFound("Image not found.");

            image.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();

            return Ok("Unit image updated successfully.");
        }

        // DELETE: api/UnitImages/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.UnitImages
                .FindAsync(id);

            if (image == null)
                return NotFound("Image not found.");

            _context.UnitImages.Remove(image);

            await _context.SaveChangesAsync();

            return Ok("Unit image deleted successfully.");
        }
    }
}