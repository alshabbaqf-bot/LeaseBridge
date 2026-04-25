using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public int? MaintenanceRequestId { get; set; }

    public int? ApplicationId { get; set; }

    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public string NotificationType { get; set; } = null!;

    public string? TargetUrl { get; set; }

    public virtual Application? Application { get; set; }

    public virtual MaintenanceRequest? MaintenanceRequest { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
