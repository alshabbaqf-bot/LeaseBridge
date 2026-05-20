using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Feedbacks;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        [Authorize(Roles = "Manager,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.Tenant)
                .Select(f => new FeedbackDto
                {
                    FeedbackId = f.FeedbackId,
                    TenantId = f.TenantId,
                    TenantName = f.Tenant.FirstName + " " + f.Tenant.LastName,
                    RequestId = f.RequestId,
                    Message = f.Message,
                    Rating = f.Rating,
                    CreatedAt = f.CreatedAt
                })
                .ToListAsync();

            return Ok(feedbacks);
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _context.Feedbacks
                .Include(f => f.Tenant)
                .Where(f => f.FeedbackId == id)
                .Select(f => new FeedbackDto
                {
                    FeedbackId = f.FeedbackId,
                    TenantId = f.TenantId,
                    TenantName = f.Tenant.FirstName + " " + f.Tenant.LastName,
                    RequestId = f.RequestId,
                    Message = f.Message,
                    Rating = f.Rating,
                    CreatedAt = f.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (feedback == null)
                return NotFound("Feedback not found.");

            return Ok(feedback);
        }

        // POST: api/Feedbacks
        [Authorize(Roles = "Tenant")]
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(
            CreateFeedbackDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check tenant exists
            var tenantExists = await _context.AppUsers
                .AnyAsync(t => t.UserId == dto.TenantId);

            if (!tenantExists)
                return BadRequest("Tenant not found.");

            // Optional maintenance request validation
            if (dto.RequestId.HasValue)
            {
                var request = await _context.MaintenanceRequests
                    .FirstOrDefaultAsync(r =>
                        r.RequestId == dto.RequestId);

                if (request == null)
                    return BadRequest("Maintenance request not found.");

                // Ensure request belongs to tenant
                if (request.TenantId != dto.TenantId)
                {
                    return BadRequest(
                        "This request does not belong to the tenant.");
                }

                // Ensure request is completed
                if (request.CompletedAt == null)
                {
                    return BadRequest(
                        "Feedback can only be submitted for completed requests.");
                }

                // Prevent duplicate feedback
                var feedbackExists = await _context.Feedbacks
                    .AnyAsync(f =>
                        f.RequestId == dto.RequestId);

                if (feedbackExists)
                {
                    return BadRequest(
                        "Feedback already exists for this request.");
                }
            }

            var feedback = new Feedback
            {
                TenantId = dto.TenantId,
                RequestId = dto.RequestId,
                Message = dto.Message,
                Rating = dto.Rating,
                CreatedAt = DateTime.Now
            };

            _context.Feedbacks.Add(feedback);

            await _context.SaveChangesAsync();

            return Ok("Feedback submitted successfully.");
        }

        // PUT: api/Feedbacks/5
        [Authorize(Roles = "Tenant")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(
            int id,
            UpdateFeedbackDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedback = await _context.Feedbacks
                .FindAsync(id);

            if (feedback == null)
                return NotFound("Feedback not found.");

            feedback.Message = dto.Message;
            feedback.Rating = dto.Rating;

            await _context.SaveChangesAsync();

            return Ok("Feedback updated successfully.");
        }

        // DELETE: api/Feedbacks/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks
                .FindAsync(id);

            if (feedback == null)
                return NotFound("Feedback not found.");

            _context.Feedbacks.Remove(feedback);

            await _context.SaveChangesAsync();

            return Ok("Feedback deleted successfully.");
        }
    }
}