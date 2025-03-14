﻿namespace DomainModel.Entities.Users;
public partial class User
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? VerificationCode { get; set; } = string.Empty;

    public DateTimeOffset? ExpirationTime { get; set; }

    public DateTimeOffset? CreateOn { get; set; }
    public string PasswordHash { get; set; } = null!;


    //public string? ConcurrencyStamp { get; set; }

    //public string? SecurityStamp { get; set; }

    //public string? UserName { get; set; } = string.Empty!;
    
    //public bool TwoFactorEnabled { get; set; }

    //public DateTimeOffset? LockoutEnd { get; set; }

    //public bool LockoutEnabled { get; set; }

    //public int AccessFailedCount { get; set; }

    //public string? NormalizedUserName { get; set; }


    //public string? NormalizedEmail { get; set; }


    public virtual Patient? Patient { get; set; }


    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}