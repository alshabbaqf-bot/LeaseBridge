using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Property
{
    public int PropertyId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public int ManagerId { get; set; }

    public virtual AppUser Manager { get; set; } = null!;

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
