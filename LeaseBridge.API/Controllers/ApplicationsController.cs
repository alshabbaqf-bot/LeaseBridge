using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Applications;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _context.Applications
                .Select(a => new ApplicationDto
                {
                    ApplicationId = a.ApplicationId,
                    TenantId = a.TenantId,
                    UnitId = a.UnitId,
                    ApplicationDate = a.ApplicationDate,
                    StatusId = a.StatusId,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                })
                .ToListAsync();

            return Ok(applications);
        }

        // GET: api/Applications/5
        [Authorize(Roles = "Manager,Tenant")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            var application = await _context.Applications
                .Where(a => a.ApplicationId == id)
                .Select(a => new ApplicationDto
                {
                    ApplicationId = a.ApplicationId,
                    TenantId = a.TenantId,
                    UnitId = a.UnitId,
                    ApplicationDate = a.ApplicationDate,
                    StatusId = a.StatusId,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (application == null)
                return NotFound("Application not found.");

            return Ok(application);
        }

        // GET: api/Applications/tenant/3
        [Authorize(Roles = "Manager,Tenant")]
        [HttpGet("tenant/{tenantId}")]
        public async Task<IActionResult> GetApplicationsByTenant(
            int tenantId)
        {
            var applications = await _context.Applications
                .Where(a => a.TenantId == tenantId)
                .Select(a => new ApplicationDto
                {
                    ApplicationId = a.ApplicationId,
                    TenantId = a.TenantId,
                    UnitId = a.UnitId,
                    ApplicationDate = a.ApplicationDate,
                    StatusId = a.StatusId,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                })
                .ToListAsync();

            return Ok(applications);
        }

        // POST: api/Applications
        [Authorize(Roles = "Tenant")]
        [HttpPost]
        public async Task<IActionResult> CreateApplication(
            CreateApplicationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate tenant exists
            var tenantExists = await _context.AppUsers
                .AnyAsync(t => t.UserId == dto.TenantId);

            if (!tenantExists)
                return BadRequest("Tenant does not exist.");

            // Validate unit exists
            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.UnitId == dto.UnitId);

            if (unit == null)
                return BadRequest("Unit does not exist.");

            // Business logic:
            // Only available units can receive applications
            if (unit.StatusId != 1)
            {
                return BadRequest(
                    "Applications are only allowed for available units.");
            }

            // Prevent duplicate pending applications
            var duplicateApplication = await _context.Applications
                .AnyAsync(a =>
                    a.TenantId == dto.TenantId &&
                    a.UnitId == dto.UnitId &&
                    a.StatusId == 1);

            if (duplicateApplication)
            {
                return BadRequest(
                    "You already submitted a pending application for this unit.");
            }

            var application = new Application
            {
                TenantId = dto.TenantId,
                UnitId = dto.UnitId,
                ApplicationDate = DateTime.Now,
                StatusId = 1, // Pending
                CreatedAt = DateTime.Now
            };

            _context.Applications.Add(application);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Application submitted successfully.",
                ApplicationId = application.ApplicationId
            });
        }

        // PUT: api/Applications/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(
            int id,
            UpdateApplicationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var application = await _context.Applications
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
                return NotFound("Application not found.");

            application.StatusId = dto.StatusId;
            application.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Application updated successfully.");
        }

        // DELETE: api/Applications/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
                return NotFound("Application not found.");

            _context.Applications.Remove(application);

            await _context.SaveChangesAsync();

            return Ok("Application deleted successfully.");
        }
    }
}