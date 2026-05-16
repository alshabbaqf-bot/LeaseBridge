using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Units;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await _context.Units
                .Select(u => new UnitDto
                {
                    UnitId = u.UnitId,
                    PropertyId = u.PropertyId,
                    UnitNumber = u.UnitNumber,
                    TypeId = u.TypeId,
                    RentAmount = u.RentAmount,
                    StatusId = u.StatusId,
                    Size = u.Size
                })
                .ToListAsync();

            return Ok(units);
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit(int id)
        {
            var unit = await _context.Units
                .Where(u => u.UnitId == id)
                .Select(u => new UnitDto
                {
                    UnitId = u.UnitId,
                    PropertyId = u.PropertyId,
                    UnitNumber = u.UnitNumber,
                    TypeId = u.TypeId,
                    RentAmount = u.RentAmount,
                    StatusId = u.StatusId,
                    Size = u.Size
                })
                .FirstOrDefaultAsync();

            if (unit == null)
                return NotFound("Unit not found.");

            return Ok(unit);
        }

        // GET: api/Units/property/1
        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetUnitsByProperty(int propertyId)
        {
            var units = await _context.Units
                .Where(u => u.PropertyId == propertyId)
                .Select(u => new UnitDto
                {
                    UnitId = u.UnitId,
                    PropertyId = u.PropertyId,
                    UnitNumber = u.UnitNumber,
                    TypeId = u.TypeId,
                    RentAmount = u.RentAmount,
                    StatusId = u.StatusId,
                    Size = u.Size
                })
                .ToListAsync();

            return Ok(units);
        }

        // POST: api/Units
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateUnit(CreateUnitDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if property exists
            var propertyExists = await _context.Properties
                .AnyAsync(p => p.PropertyId == dto.PropertyId);

            if (!propertyExists)
                return BadRequest("Property does not exist.");

            var unit = new Unit
            {
                PropertyId = dto.PropertyId,
                UnitNumber = dto.UnitNumber,
                TypeId = dto.TypeId,
                RentAmount = dto.RentAmount,
                StatusId = dto.StatusId,
                Size = dto.Size
            };

            _context.Units.Add(unit);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Unit created successfully.",
                UnitId = unit.UnitId
            });
        }

        // PUT: api/Units/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(
            int id,
            UpdateUnitDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.UnitId == id);

            if (unit == null)
                return NotFound("Unit not found.");

            unit.PropertyId = dto.PropertyId;
            unit.UnitNumber = dto.UnitNumber;
            unit.TypeId = dto.TypeId;
            unit.RentAmount = dto.RentAmount;
            unit.StatusId = dto.StatusId;
            unit.Size = dto.Size;

            await _context.SaveChangesAsync();

            return Ok("Unit updated successfully.");
        }

        // DELETE: api/Units/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.UnitId == id);

            if (unit == null)
                return NotFound("Unit not found.");

            _context.Units.Remove(unit);

            await _context.SaveChangesAsync();

            return Ok("Unit deleted successfully.");
        }
    }
}