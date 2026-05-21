using System;
using System.Collections.Generic;

namespace LeaseBridge.API.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }

        public int LeaseId { get; set; }

        public int? PaymentId { get; set; }

        public string InvoiceNumber { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime DueDate { get; set; }

        public int StatusId { get; set; }

        public virtual InvoiceStatus Status { get; set; } = null!;

        public virtual Lease Lease { get; set; } = null!;

        public virtual Payment? Payment { get; set; }


    }
}