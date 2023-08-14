using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Users;

public class UserLoginDto
{
    public string? Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; } = string.Empty;

    public string Password { get; set; } = null!;
}
