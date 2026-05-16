using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Leases;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeasesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Leases
        [HttpGet]
        public async Task<IActionResult> GetAllLeases()
        {
            var leases = await _context.Leases
                .Select(l => new LeaseDto
                {
                    LeaseId = l.LeaseId,
                    TenantId = l.TenantId,
                    UnitId = l.UnitId,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    StatusId = l.StatusId,
                    IsActive = l.IsActive
                })
                .ToListAsync();

            return Ok(leases);
        }

        // GET: api/Leases/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLease(int id)
        {
            var lease = await _context.Leases
                .Where(l => l.LeaseId == id)
                .Select(l => new LeaseDto
                {
                    LeaseId = l.LeaseId,
                    TenantId = l.TenantId,
                    UnitId = l.UnitId,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    StatusId = l.StatusId,
                    IsActive = l.IsActive
                })
                .FirstOrDefaultAsync();

            if (lease == null)
                return NotFound("Lease not found.");

            return Ok(lease);
        }

        // POST: api/Leases
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateLease(CreateLeaseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate dates
            if (dto.EndDate <= dto.StartDate)
                return BadRequest("End date must be after start date.");

            // Check tenant exists
            var tenantExists = await _context.AppUsers
                .AnyAsync(t => t.UserId == dto.TenantId);

            if (!tenantExists)
                return BadRequest("Tenant does not exist.");

            // Check unit exists
            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.UnitId == dto.UnitId);

            if (unit == null)
                return BadRequest("Unit does not exist.");

            // Check if unit already has active lease
            var activeLeaseExists = await _context.Leases
                .AnyAsync(l =>
                    l.UnitId == dto.UnitId &&
                    l.IsActive);

            if (activeLeaseExists)
                return BadRequest("Unit already has an active lease.");

            // Create lease
            var lease = new Lease
            {
                TenantId = dto.TenantId,
                UnitId = dto.UnitId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                StatusId = dto.StatusId,
                IsActive = dto.IsActive
            };

            _context.Leases.Add(lease);

            // Update unit status to Occupied
            unit.StatusId = 3;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Lease created successfully.",
                LeaseId = lease.LeaseId
            });
        }

        // PUT: api/Leases/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLease(
            int id,
            UpdateLeaseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.EndDate <= dto.StartDate)
                return BadRequest("End date must be after start date.");

            var lease = await _context.Leases
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
                return NotFound("Lease not found.");

            lease.TenantId = dto.TenantId;
            lease.UnitId = dto.UnitId;
            lease.StartDate = dto.StartDate;
            lease.EndDate = dto.EndDate;
            lease.StatusId = dto.StatusId;
            lease.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return Ok("Lease updated successfully.");
        }

        // DELETE: api/Leases/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLease(int id)
        {
            var lease = await _context.Leases
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
                return NotFound("Lease not found.");

            // Reset unit status to Available
            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.UnitId == lease.UnitId);

            if (unit != null)
            {
                unit.StatusId = 1;
            }

            _context.Leases.Remove(lease);

            await _context.SaveChangesAsync();

            return Ok("Lease deleted successfully.");
        }
    }
}