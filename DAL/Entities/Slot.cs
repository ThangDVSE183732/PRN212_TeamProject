using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Slot
{
    public int SlotId { get; set; }

    public int ShiftId { get; set; }

    public TimeOnly? StartTime { get; set; }

    public string? Status { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Shift Shift { get; set; } = null!;

    public string DisplayTime
    {
        get
        {
            if(StartTime.HasValue && EndTime.HasValue)
            {
                return  $"{StartTime.Value.ToString("HH:mm")} - {EndTime.Value.ToString("HH:mm")}";
            }else
            {
                return "No Time Set";
            }
        }
    }
}
