using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int PatientId { get; set; }

    public int ServiceId { get; set; }

    public int SlotId { get; set; }

    public DateTime DateBooking { get; set; }

    public string? Note { get; set; }

    public string Result { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Service Service { get; set; } = null!;

    public virtual Slot Slot { get; set; } = null!;
}
