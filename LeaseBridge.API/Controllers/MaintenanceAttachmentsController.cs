using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.MaintenanceAttachments;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceAttachmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceAttachmentsController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaintenanceAttachments
        [Authorize(Roles = "Manager,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAllAttachments()
        {
            var attachments = await _context.MaintenanceAttachments
                .Select(a => new MaintenanceAttachmentDto
                {
                    AttachmentId = a.AttachmentId,
                    RequestId = a.RequestId,
                    FileUrl = a.FileUrl
                })
                .ToListAsync();

            return Ok(attachments);
        }

        // GET: api/MaintenanceAttachments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttachmentById(int id)
        {
            var attachment = await _context.MaintenanceAttachments
                .Where(a => a.AttachmentId == id)
                .Select(a => new MaintenanceAttachmentDto
                {
                    AttachmentId = a.AttachmentId,
                    RequestId = a.RequestId,
                    FileUrl = a.FileUrl
                })
                .FirstOrDefaultAsync();

            if (attachment == null)
                return NotFound("Attachment not found.");

            return Ok(attachment);
        }

        // GET: api/MaintenanceAttachments/request/5
        [HttpGet("request/{requestId}")]
        public async Task<IActionResult> GetAttachmentsByRequest(
            int requestId)
        {
            var requestExists = await _context.MaintenanceRequests
                .AnyAsync(r => r.RequestId == requestId);

            if (!requestExists)
                return NotFound("Maintenance request not found.");

            var attachments = await _context.MaintenanceAttachments
                .Where(a => a.RequestId == requestId)
                .Select(a => new MaintenanceAttachmentDto
                {
                    AttachmentId = a.AttachmentId,
                    RequestId = a.RequestId,
                    FileUrl = a.FileUrl
                })
                .ToListAsync();

            return Ok(attachments);
        }

        // POST: api/MaintenanceAttachments
        [Authorize(Roles = "Manager,Staff,Tenant")]
        [HttpPost]
        public async Task<IActionResult> CreateAttachment(
            CreateMaintenanceAttachmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var requestExists = await _context.MaintenanceRequests
                .AnyAsync(r => r.RequestId == dto.RequestId);

            if (!requestExists)
                return BadRequest("Maintenance request not found.");

            var attachment = new MaintenanceAttachment
            {
                RequestId = dto.RequestId,
                FileUrl = dto.FileUrl
            };

            _context.MaintenanceAttachments.Add(attachment);

            await _context.SaveChangesAsync();

            return Ok("Attachment added successfully.");
        }

        // PUT: api/MaintenanceAttachments/5
        [Authorize(Roles = "Manager,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttachment(
            int id,
            UpdateMaintenanceAttachmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attachment = await _context.MaintenanceAttachments
                .FindAsync(id);

            if (attachment == null)
                return NotFound("Attachment not found.");

            attachment.FileUrl = dto.FileUrl;

            await _context.SaveChangesAsync();

            return Ok("Attachment updated successfully.");
        }

        // DELETE: api/MaintenanceAttachments/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var attachment = await _context.MaintenanceAttachments
                .FindAsync(id);

            if (attachment == null)
                return NotFound("Attachment not found.");

            _context.MaintenanceAttachments.Remove(attachment);

            await _context.SaveChangesAsync();

            return Ok("Attachment deleted successfully.");
        }
    }
}