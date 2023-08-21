using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class EmployeeAccount
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? UserName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTimeOffset? CreateOn { get; set; }

    public string? PasswordHash { get; set; }
}
