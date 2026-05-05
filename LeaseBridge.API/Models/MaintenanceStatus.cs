using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class MaintenanceStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    public virtual ICollection<MaintenanceUpdate> MaintenanceUpdateNewStatuses { get; set; } = new List<MaintenanceUpdate>();

    public virtual ICollection<MaintenanceUpdate> MaintenanceUpdateOldStatuses { get; set; } = new List<MaintenanceUpdate>();
}
