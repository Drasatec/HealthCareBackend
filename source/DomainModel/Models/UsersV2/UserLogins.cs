namespace DomainModel.Models.UsersV2;

public class UserLoginByEmail
{
    public string? Email { get; set; }

    public string Password { get; set; } = null!;
}

public class UserLoginByPhone
{
    public string? PhoneNumber { get; set; } = string.Empty;

    public string Password { get; set; } = null!;
}

public class UserLoginByUserName
{
    public string? UserName { get; set; } = string.Empty;

    public string Password { get; set; } = null!;
}
