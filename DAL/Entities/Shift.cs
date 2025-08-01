using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Shift
{
    public int ShiftId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly DateWork { get; set; }

    public string Name { get; set; } = null!;

    public TimeOnly? StartTime { get; set; }

    public string? Status { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public string DisplayTime
    {
        get
        {
            if (StartTime.HasValue && EndTime.HasValue)
            {
                return $"{StartTime.Value.ToString("HH:mm")} - {EndTime.Value.ToString("HH:mm")}";
            }
            else
            {
                return "No Time Set";
            }
        }
    }
}
