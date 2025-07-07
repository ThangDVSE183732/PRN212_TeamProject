using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Address { get; set; }

    public string? Status { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public virtual Role Role { get; set; } = null!;
}
