using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Users;

public class ApplicationUser
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? NormalizedUserName { get; set; }

    public string Email { get; set; } = null!;

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }
}
