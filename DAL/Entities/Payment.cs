using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? BookingId { get; set; }

    public string? TotalAmount { get; set; }

    public string? Status { get; set; }

    public string? Method { get; set; }

    public virtual Booking? Booking { get; set; }
}
