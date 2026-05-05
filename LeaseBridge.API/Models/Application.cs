using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int TenantId { get; set; }

    public int UnitId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ApplicationStatus Status { get; set; } = null!;

    public virtual AppUser Tenant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
