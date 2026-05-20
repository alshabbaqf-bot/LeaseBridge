using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.MaintenanceUpdates;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceUpdatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceUpdatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaintenanceUpdates
        [Authorize(Roles = "Manager,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAllUpdates()
        {
            var updates = await _context.MaintenanceUpdates
                .Include(mu => mu.OldStatus)
                .Include(mu => mu.NewStatus)
                .Include(mu => mu.UpdatedByNavigation)
                .Select(mu => new MaintenanceUpdateDto
                {
                    UpdateId = mu.UpdateId,
                    RequestId = mu.RequestId,
                    OldStatusId = mu.OldStatusId,
                    OldStatusName = mu.OldStatus != null
                        ? mu.OldStatus.Name
                        : null,
                    NewStatusId = mu.NewStatusId,
                    NewStatusName = mu.NewStatus.Name,
                    UpdatedBy = mu.UpdatedBy,
                    UpdatedByName =
                        mu.UpdatedByNavigation.FirstName + " " +
                        mu.UpdatedByNavigation.LastName,
                    Notes = mu.Notes,
                    CreatedAt = mu.CreatedAt
                })
                .ToListAsync();

            return Ok(updates);
        }

        // GET: api/MaintenanceUpdates/request/5
        [HttpGet("request/{requestId}")]
        public async Task<IActionResult> GetUpdatesByRequest(
            int requestId)
        {
            var requestExists = await _context.MaintenanceRequests
                .AnyAsync(r => r.RequestId == requestId);

            if (!requestExists)
                return NotFound("Maintenance request not found.");

            var updates = await _context.MaintenanceUpdates
                .Include(mu => mu.OldStatus)
                .Include(mu => mu.NewStatus)
                .Include(mu => mu.UpdatedByNavigation)
                .Where(mu => mu.RequestId == requestId)
                .Select(mu => new MaintenanceUpdateDto
                {
                    UpdateId = mu.UpdateId,
                    RequestId = mu.RequestId,
                    OldStatusId = mu.OldStatusId,
                    OldStatusName = mu.OldStatus != null
                        ? mu.OldStatus.Name
                        : null,
                    NewStatusId = mu.NewStatusId,
                    NewStatusName = mu.NewStatus.Name,
                    UpdatedBy = mu.UpdatedBy,
                    UpdatedByName =
                        mu.UpdatedByNavigation.FirstName + " " +
                        mu.UpdatedByNavigation.LastName,
                    Notes = mu.Notes,
                    CreatedAt = mu.CreatedAt
                })
                .OrderByDescending(mu => mu.CreatedAt)
                .ToListAsync();

            return Ok(updates);
        }

        // POST: api/MaintenanceUpdates
        [Authorize(Roles = "Manager,Staff")]
        [HttpPost]
        public async Task<IActionResult> CreateUpdate(
            CreateMaintenanceUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check request exists
            var request = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(r =>
                    r.RequestId == dto.RequestId);

            if (request == null)
                return BadRequest("Maintenance request not found.");

            // Check updater exists
            var updaterExists = await _context.AppUsers
                .AnyAsync(u => u.UserId == dto.UpdatedBy);

            if (!updaterExists)
                return BadRequest("UpdatedBy user not found.");

            // Check new status exists
            var newStatusExists = await _context.MaintenanceStatuses
                .AnyAsync(s => s.StatusId == dto.NewStatusId);

            if (!newStatusExists)
                return BadRequest("New status not found.");

            // Check old status exists if provided
            if (dto.OldStatusId.HasValue)
            {
                var oldStatusExists = await _context.MaintenanceStatuses
                    .AnyAsync(s =>
                        s.StatusId == dto.OldStatusId);

                if (!oldStatusExists)
                    return BadRequest("Old status not found.");
            }

            var maintenanceUpdate = new MaintenanceUpdate
            {
                RequestId = dto.RequestId,
                OldStatusId = dto.OldStatusId,
                NewStatusId = dto.NewStatusId,
                UpdatedBy = dto.UpdatedBy,
                Notes = dto.Notes,
                CreatedAt = DateTime.Now
            };

            _context.MaintenanceUpdates.Add(maintenanceUpdate);

            // Update request status automatically
            request.StatusId = dto.NewStatusId;

            // Auto-set completion date if completed
            if (dto.NewStatusId == 3)
            {
                request.CompletedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return Ok("Maintenance update added successfully.");
        }

        // PUT: api/MaintenanceUpdates/5
        [Authorize(Roles = "Manager,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenanceUpdate(
            int id,
            UpdateMaintenanceUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var update = await _context.MaintenanceUpdates
                .FindAsync(id);

            if (update == null)
                return NotFound("Maintenance update not found.");

            var statusExists = await _context.MaintenanceStatuses
                .AnyAsync(s => s.StatusId == dto.NewStatusId);

            if (!statusExists)
                return BadRequest("Status not found.");

            update.NewStatusId = dto.NewStatusId;
            update.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return Ok("Maintenance update updated successfully.");
        }

        // DELETE: api/MaintenanceUpdates/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUpdate(int id)
        {
            var update = await _context.MaintenanceUpdates
                .FindAsync(id);

            if (update == null)
                return NotFound("Maintenance update not found.");

            _context.MaintenanceUpdates.Remove(update);

            await _context.SaveChangesAsync();

            return Ok("Maintenance update deleted successfully.");
        }
    }
}