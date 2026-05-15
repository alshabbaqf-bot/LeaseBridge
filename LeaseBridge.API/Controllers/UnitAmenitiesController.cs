using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.UnitAmenities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitAmenitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitAmenitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitAmenities
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var unitAmenities = await _context.Units
                .SelectMany(u => u.Amenities.Select(a =>
                    new UnitAmenityDto
                    {
                        UnitId = u.UnitId,
                        AmenityId = a.AmenityId,
                        AmenityName = a.Name
                    }))
                .ToListAsync();

            return Ok(unitAmenities);
        }

        // GET: api/UnitAmenities/unit/1
        [HttpGet("unit/{unitId}")]
        public async Task<IActionResult> GetAmenitiesByUnit(int unitId)
        {
            var unit = await _context.Units
                .Include(u => u.Amenities)
                .FirstOrDefaultAsync(u => u.UnitId == unitId);

            if (unit == null)
                return NotFound("Unit not found.");

            var amenities = unit.Amenities
                .Select(a => new UnitAmenityDto
                {
                    UnitId = unit.UnitId,
                    AmenityId = a.AmenityId,
                    AmenityName = a.Name
                });

            return Ok(amenities);
        }

        // POST: api/UnitAmenities
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> AddAmenity(
            CreateUnitAmenityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var unit = await _context.Units
                .Include(u => u.Amenities)
                .FirstOrDefaultAsync(u => u.UnitId == dto.UnitId);

            if (unit == null)
                return BadRequest("Unit not found.");

            var amenity = await _context.Amenities
                .FirstOrDefaultAsync(a => a.AmenityId == dto.AmenityId);

            if (amenity == null)
                return BadRequest("Amenity not found.");

            // Prevent duplicates
            if (unit.Amenities.Any(a =>
                a.AmenityId == dto.AmenityId))
            {
                return BadRequest(
                    "Amenity already assigned to this unit.");
            }

            unit.Amenities.Add(amenity);

            await _context.SaveChangesAsync();

            return Ok("Amenity assigned successfully.");
        }

        // DELETE: api/UnitAmenities
        [Authorize(Roles = "Manager")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAmenity(
            int unitId,
            int amenityId)
        {
            var unit = await _context.Units
                .Include(u => u.Amenities)
                .FirstOrDefaultAsync(u => u.UnitId == unitId);

            if (unit == null)
                return NotFound("Unit not found.");

            var amenity = unit.Amenities
                .FirstOrDefault(a =>
                    a.AmenityId == amenityId);

            if (amenity == null)
                return NotFound("Amenity not assigned.");

            unit.Amenities.Remove(amenity);

            await _context.SaveChangesAsync();

            return Ok("Amenity removed successfully.");
        }
    }
}