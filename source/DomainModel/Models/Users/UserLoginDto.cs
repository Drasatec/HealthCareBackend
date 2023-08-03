using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Users;

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
