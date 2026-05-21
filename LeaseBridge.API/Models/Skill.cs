using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StaffSkill> StaffSkills { get; set; } = new List<StaffSkill>();
}
