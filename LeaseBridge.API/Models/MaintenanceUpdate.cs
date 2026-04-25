using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class MaintenanceUpdate
{
    public int UpdateId { get; set; }

    public int RequestId { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public int UpdatedBy { get; set; }

    public virtual MaintenanceStatus NewStatus { get; set; } = null!;

    public virtual MaintenanceStatus? OldStatus { get; set; }

    public virtual MaintenanceRequest Request { get; set; } = null!;

    public virtual AppUser UpdatedByNavigation { get; set; } = null!;
}
