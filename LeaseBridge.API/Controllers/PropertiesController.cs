using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Properties;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Properties
        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _context.Properties
                .Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    Name = p.Name,
                    Location = p.Location,
                    Description = p.Description,
                    ManagerId = p.ManagerId
                })
                .ToListAsync();

            return Ok(properties);
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _context.Properties
                .Where(p => p.PropertyId == id)
                .Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    Name = p.Name,
                    Location = p.Location,
                    Description = p.Description,
                    ManagerId = p.ManagerId
                })
                .FirstOrDefaultAsync();

            if (property == null)
                return NotFound("Property not found.");

            return Ok(property);
        }

        // POST: api/Properties
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get logged-in user email from JWT token
            var email = User.FindFirstValue(ClaimTypes.Email)
                ?? User.FindFirstValue(ClaimTypes.Name);

            // Find manager in AppUsers table
            var manager = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Email == email);

            if (manager == null)
                return Unauthorized();

            // Create property
            var property = new Property
            {
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                ManagerId = manager.UserId
            };

            _context.Properties.Add(property);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Property created successfully.",
                PropertyId = property.PropertyId
            });
        }

        // PUT: api/Properties/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(
            int id,
            UpdatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (property == null)
                return NotFound("Property not found.");

            property.Name = dto.Name;
            property.Location = dto.Location;
            property.Description = dto.Description;

            await _context.SaveChangesAsync();

            return Ok("Property updated successfully.");
        }

        // DELETE: api/Properties/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (property == null)
                return NotFound("Property not found.");

            _context.Properties.Remove(property);

            await _context.SaveChangesAsync();

            return Ok("Property deleted successfully.");
        }
    }
}