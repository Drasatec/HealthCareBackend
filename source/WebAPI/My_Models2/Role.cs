using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
