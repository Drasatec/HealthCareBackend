using DomainModel.Entities.SettingsEntities;
namespace DomainModel.Models.Admins;

public class EmployeeAccountDto
{
    //public string Id { get; set; } = string.Empty;

    public string FullName { get; set; } = null!;

    public string? UserName { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; } = string.Empty;

    public string Password { get; set; } = null!;

    public static implicit operator EmployeeAccount(EmployeeAccountDto dto)
    {
        return new EmployeeAccount()
        {
            Id = Guid.NewGuid().ToString(),
            FullName = dto.FullName,
            Email = dto.Email?.ToLower(),
            UserName = dto.UserName,
            PhoneNumber = dto.PhoneNumber,
        };
    }
}
