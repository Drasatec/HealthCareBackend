namespace DomainModel.Entities.Users;


public partial class User
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = string.Empty!;

    //public string? NormalizedUserName { get; set; }

    public string Email { get; set; } = string.Empty!;

    //public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public string VerificationCode { get; set; } = string.Empty;

    public DateTime? ExpirationTime { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    //public virtual Patient? Patient { get; set; }


    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}