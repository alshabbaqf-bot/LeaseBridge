using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.MaintenanceAssignments;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceAssignmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceAssignmentsController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaintenanceAssignments
        [Authorize(Roles = "Manager,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _context.MaintenanceAssignments
                .Select(a => new MaintenanceAssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    RequestId = a.RequestId,
                    StaffId = a.StaffId,
                    AssignedDate = a.AssignedDate
                })
                .ToListAsync();

            return Ok(assignments);
        }

        // GET: api/MaintenanceAssignments/5
        [Authorize(Roles = "Manager,Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignment(int id)
        {
            var assignment = await _context.MaintenanceAssignments
                .Where(a => a.AssignmentId == id)
                .Select(a => new MaintenanceAssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    RequestId = a.RequestId,
                    StaffId = a.StaffId,
                    AssignedDate = a.AssignedDate
                })
                .FirstOrDefaultAsync();

            if (assignment == null)
                return NotFound("Assignment not found.");

            return Ok(assignment);
        }

        // GET: api/MaintenanceAssignments/staff/2
        [Authorize(Roles = "Manager,Staff")]
        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetAssignmentsByStaff(
            int staffId)
        {
            var assignments = await _context.MaintenanceAssignments
                .Where(a => a.StaffId == staffId)
                .Select(a => new MaintenanceAssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    RequestId = a.RequestId,
                    StaffId = a.StaffId,
                    AssignedDate = a.AssignedDate
                })
                .ToListAsync();

            return Ok(assignments);
        }

        // POST: api/MaintenanceAssignments
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateAssignment(
            CreateMaintenanceAssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check request exists
            var requestExists = await _context.MaintenanceRequests
                .AnyAsync(r => r.RequestId == dto.RequestId);

            if (!requestExists)
                return BadRequest("Maintenance request does not exist.");

            // Check staff exists
            var staffExists = await _context.AppUsers
                .AnyAsync(s => s.UserId == dto.StaffId);

            if (!staffExists)
                return BadRequest("Staff user does not exist.");

            // Prevent duplicate assignment
            var alreadyAssigned = await _context.MaintenanceAssignments
                .AnyAsync(a =>
                    a.RequestId == dto.RequestId &&
                    a.StaffId == dto.StaffId);

            if (alreadyAssigned)
                return BadRequest("Staff already assigned to this request.");

            var assignment = new MaintenanceAssignment
            {
                RequestId = dto.RequestId,
                StaffId = dto.StaffId,
                AssignedDate = DateTime.Now
            };

            _context.MaintenanceAssignments.Add(assignment);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Maintenance assignment created successfully.",
                AssignmentId = assignment.AssignmentId
            });
        }

        // PUT: api/MaintenanceAssignments/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(
            int id,
            UpdateMaintenanceAssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var assignment = await _context.MaintenanceAssignments
                .FirstOrDefaultAsync(a => a.AssignmentId == id);

            if (assignment == null)
                return NotFound("Assignment not found.");

            // Check staff exists
            var staffExists = await _context.AppUsers
                .AnyAsync(s => s.UserId == dto.StaffId);

            if (!staffExists)
                return BadRequest("Staff user does not exist.");

            assignment.StaffId = dto.StaffId;

            await _context.SaveChangesAsync();

            return Ok("Assignment updated successfully.");
        }

        // DELETE: api/MaintenanceAssignments/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var assignment = await _context.MaintenanceAssignments
                .FirstOrDefaultAsync(a => a.AssignmentId == id);

            if (assignment == null)
                return NotFound("Assignment not found.");

            _context.MaintenanceAssignments.Remove(assignment);

            await _context.SaveChangesAsync();

            return Ok("Assignment deleted successfully.");
        }
    }
}