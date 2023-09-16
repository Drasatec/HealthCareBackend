namespace DomainModel.Models.UsersV2;

public class UserAccountDto
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string UserType { get; set; } = string.Empty;
}
