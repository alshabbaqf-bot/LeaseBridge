using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.MaintenanceRequests;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaintenanceRequests
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _context.MaintenanceRequests
                .Select(r => new MaintenanceRequestDto
                {
                    RequestId = r.RequestId,
                    TenantId = r.TenantId,
                    UnitId = r.UnitId,
                    CategoryId = r.CategoryId,
                    TicketNumber = r.TicketNumber,
                    Title = r.Title,
                    Description = r.Description,
                    PriorityId = r.PriorityId,
                    StatusId = r.StatusId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    CompletedAt = r.CompletedAt
                })
                .ToListAsync();

            return Ok(requests);
        }

        // GET: api/MaintenanceRequests/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var request = await _context.MaintenanceRequests
                .Where(r => r.RequestId == id)
                .Select(r => new MaintenanceRequestDto
                {
                    RequestId = r.RequestId,
                    TenantId = r.TenantId,
                    UnitId = r.UnitId,
                    CategoryId = r.CategoryId,
                    TicketNumber = r.TicketNumber,
                    Title = r.Title,
                    Description = r.Description,
                    PriorityId = r.PriorityId,
                    StatusId = r.StatusId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    CompletedAt = r.CompletedAt
                })
                .FirstOrDefaultAsync();

            if (request == null)
                return NotFound("Maintenance request not found.");

            return Ok(request);
        }

        // POST: api/MaintenanceRequests
        [Authorize(Roles = "Tenant,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateRequest(
            CreateMaintenanceRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate tenant exists
            var tenantExists = await _context.AppUsers
                .AnyAsync(t => t.UserId == dto.TenantId);

            if (!tenantExists)
                return BadRequest("Tenant does not exist.");

            // Validate unit exists
            var unitExists = await _context.Units
                .AnyAsync(u => u.UnitId == dto.UnitId);

            if (!unitExists)
                return BadRequest("Unit does not exist.");

            // Generate ticket number
            var ticketNumber = $"MR-{DateTime.Now.Ticks}";

            var request = new MaintenanceRequest
            {
                TenantId = dto.TenantId,
                UnitId = dto.UnitId,
                CategoryId = dto.CategoryId,
                TicketNumber = ticketNumber,
                Title = dto.Title,
                Description = dto.Description,
                PriorityId = dto.PriorityId,
                StatusId = dto.StatusId,
                CreatedAt = DateTime.Now
            };

            _context.MaintenanceRequests.Add(request);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Maintenance request created successfully.",
                RequestId = request.RequestId
            });
        }

        // PUT: api/MaintenanceRequests/5
        [Authorize(Roles = "Manager,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(
            int id,
            UpdateMaintenanceRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (request == null)
                return NotFound("Maintenance request not found.");

            request.CategoryId = dto.CategoryId;
            request.Title = dto.Title;
            request.Description = dto.Description;
            request.PriorityId = dto.PriorityId;
            request.StatusId = dto.StatusId;
            request.CompletedAt = dto.CompletedAt;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Maintenance request updated successfully.");
        }

        // DELETE: api/MaintenanceRequests/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (request == null)
                return NotFound("Maintenance request not found.");

            _context.MaintenanceRequests.Remove(request);

            await _context.SaveChangesAsync();

            return Ok("Maintenance request deleted successfully.");
        }
    }
}