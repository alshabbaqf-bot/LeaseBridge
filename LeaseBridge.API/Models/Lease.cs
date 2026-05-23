using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Lease
{
    public int LeaseId { get; set; }

    public int TenantId { get; set; }

    public int UnitId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int StatusId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual LeaseStatus Status { get; set; } = null!;

    public virtual AppUser Tenant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
