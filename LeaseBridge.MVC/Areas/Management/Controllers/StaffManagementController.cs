using LeaseBridge.API.Data;

using LeaseBridge.API.Models;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.MVC.Areas.Management.Controllers

{

    [Area("Management")]

    [Authorize(Roles = " Manager")]

    public class StaffManagementController : Controller

    {

        private readonly ApplicationDbContext _context;

        public StaffManagementController(ApplicationDbContext context)

        {

            _context = context;

        }

        // GET: /Management/StaffManagement/Index

        public async Task<IActionResult> Index()

        {

            var staffRole = await _context.Set<IdentityRole>()

                .FirstOrDefaultAsync(r => r.Name == "Staff");

            if (staffRole == null)

            {

                return View(new List<StaffProfileViewModel>());

            }

            var staffIdentityIds = await _context.Set<IdentityUserRole<string>>()

                .Where(ur => ur.RoleId == staffRole.Id)

                .Select(ur => ur.UserId)

                .ToListAsync();

            var staffUsers = await _context.AppUsers

                .Where(u =>

                    u.IdentityUserId != null &&

                    staffIdentityIds.Contains(u.IdentityUserId))

                .OrderBy(u => u.FirstName)

                .ThenBy(u => u.LastName)

                .Select(u => new StaffProfileViewModel

                {

                    UserId = u.UserId,

                    FullName = u.FirstName + " " + u.LastName,

                    Email = u.Email,

                    PhoneNumber = u.PhoneNumber,

                    IsAvailable = u.IsAvailable == true,

                    SkillNames = _context.StaffSkills

                        .Where(ss => ss.StaffId == u.UserId)

                        .Join(

                            _context.Skills,

                            ss => ss.SkillId,

                            s => s.SkillId,

                            (ss, s) => s.Name

                        )

                        .ToList()

                })

                .ToListAsync();

            return View(staffUsers);

        }

        // POST: /Management/StaffManagement/ToggleAvailability/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ToggleAvailability(int id)

        {

            var staff = await _context.AppUsers

                .FirstOrDefaultAsync(u => u.UserId == id);

            if (staff == null)

            {

                return NotFound();

            }

            staff.IsAvailable = !(staff.IsAvailable == true);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Staff availability updated successfully.";

            return RedirectToAction(nameof(Index));

        }

        // GET: /Management/StaffManagement/ManageSkills/5

        public async Task<IActionResult> ManageSkills(int id)

        {

            var staff = await _context.AppUsers

                .FirstOrDefaultAsync(u => u.UserId == id);

            if (staff == null)

            {

                return NotFound();

            }

            var skills = await _context.Skills

                .OrderBy(s => s.Name)

                .ToListAsync();

            var selectedSkillIds = await _context.StaffSkills

                .Where(ss => ss.StaffId == id)

                .Select(ss => ss.SkillId)

                .ToListAsync();

            var model = new ManageStaffSkillsViewModel

            {

                StaffId = staff.UserId,

                StaffName = staff.FirstName + " " + staff.LastName,

                Skills = skills.Select(s => new StaffSkillCheckboxViewModel

                {

                    SkillId = s.SkillId,

                    SkillName = s.Name,

                    IsSelected = selectedSkillIds.Contains(s.SkillId)

                }).ToList()

            };

            return View(model);

        }

        // POST: /Management/StaffManagement/ManageSkills/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ManageSkills(int id, List<int> selectedSkillIds)

        {

            var staff = await _context.AppUsers

                .FirstOrDefaultAsync(u => u.UserId == id);

            if (staff == null)

            {

                return NotFound();

            }

            var existingSkills = await _context.StaffSkills

                .Where(ss => ss.StaffId == id)

                .ToListAsync();

            _context.StaffSkills.RemoveRange(existingSkills);

            if (selectedSkillIds != null && selectedSkillIds.Any())

            {

                foreach (var skillId in selectedSkillIds.Distinct())

                {

                    var skill = await _context.Skills

                        .FirstOrDefaultAsync(s => s.SkillId == skillId);

                    if (skill != null)

                    {

                        var categoryId = await _context.MaintenanceCategories

                            .Where(c => c.Name == skill.Name)

                            .Select(c => (int?)c.CategoryId)

                            .FirstOrDefaultAsync();

                        if (categoryId.HasValue)

                        {

                            _context.StaffSkills.Add(new StaffSkill

                            {

                                SkillId = skill.SkillId,

                                StaffId = id,

                                CategoryId = categoryId.Value

                            });

                        }

                    }

                }

            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Staff skills updated successfully.";

            return RedirectToAction(nameof(Index));

        }

    }

    public class StaffProfileViewModel

    {

        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public List<string> SkillNames { get; set; } = new();

    }

    public class ManageStaffSkillsViewModel

    {

        public int StaffId { get; set; }

        public string StaffName { get; set; } = string.Empty;

        public List<StaffSkillCheckboxViewModel> Skills { get; set; } = new();

    }

    public class StaffSkillCheckboxViewModel

    {

        public int SkillId { get; set; }

        public string SkillName { get; set; } = string.Empty;

        public bool IsSelected { get; set; }

    }

}
