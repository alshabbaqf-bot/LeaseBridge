using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class MaintenanceCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
}
