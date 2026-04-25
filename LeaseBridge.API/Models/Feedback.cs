using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int TenantId { get; set; }

    public string Message { get; set; } = null!;

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? RequestId { get; set; }

    public virtual MaintenanceRequest? Request { get; set; }

    public virtual AppUser Tenant { get; set; } = null!;
}
