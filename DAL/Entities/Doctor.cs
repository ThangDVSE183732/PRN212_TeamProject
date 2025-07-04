using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Status { get; set; }

    public string? Password { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
