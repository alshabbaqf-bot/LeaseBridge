using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class InvoiceStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; }
        = new List<Invoice>();
}