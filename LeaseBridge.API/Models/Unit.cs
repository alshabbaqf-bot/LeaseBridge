using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public int PropertyId { get; set; }

    public string UnitNumber { get; set; } = null!;

    public int TypeId { get; set; }

    public decimal RentAmount { get; set; }

    public int StatusId { get; set; }

    public decimal? Size { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    public virtual Property Property { get; set; } = null!;

    public virtual UnitStatus Status { get; set; } = null!;

    public virtual UnitType Type { get; set; } = null!;

    public virtual ICollection<UnitImage> UnitImages { get; set; } = new List<UnitImage>();

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}
