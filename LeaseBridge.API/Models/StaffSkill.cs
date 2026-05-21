using LeaseBridge.API.Models;

public partial class StaffSkill
{
    public int SkillId { get; set; }

    public int StaffId { get; set; }

    public int CategoryId { get; set; }

    public virtual MaintenanceCategory Category { get; set; } = null!;

    public virtual AppUser Staff { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}