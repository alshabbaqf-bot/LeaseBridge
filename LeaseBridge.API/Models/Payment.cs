using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int LeaseId { get; set; }

    public int MethodId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int StatusId { get; set; }

    public DateTime DueDate { get; set; }

    public string? TransactionReference { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Lease Lease { get; set; } = null!;

    public virtual PaymentMethod Method { get; set; } = null!;

    public virtual PaymentStatus Status { get; set; } = null!;
}
