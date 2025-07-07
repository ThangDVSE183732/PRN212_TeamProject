using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Patient
{
    public int PatientId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Password { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? BloodType { get; set; }

    public string? Status { get; set; }

    public string EmergencyPhoneNumber { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
