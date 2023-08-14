namespace DomainModel.Models.Users;
public class UserChangePassDto
{
    public string? Email { get; set; } = string.Empty;

    public string? UserId { get; set; } = string.Empty;

    public string Password { get; set; } = null!;

    public string OldPassword { get; set; } = null!;

}
