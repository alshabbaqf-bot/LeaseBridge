using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class MaintenanceRequest
{
    public int RequestId { get; set; }

    public int TenantId { get; set; }

    public int UnitId { get; set; }

    public int CategoryId { get; set; }

    public string TicketNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual MaintenanceCategory Category { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<MaintenanceAssignment> MaintenanceAssignments { get; set; } = new List<MaintenanceAssignment>();

    public virtual ICollection<MaintenanceAttachment> MaintenanceAttachments { get; set; } = new List<MaintenanceAttachment>();

    public virtual ICollection<MaintenanceUpdate> MaintenanceUpdates { get; set; } = new List<MaintenanceUpdate>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual PriorityType Priority { get; set; } = null!;

    public virtual MaintenanceStatus Status { get; set; } = null!;

    public virtual AppUser Tenant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
