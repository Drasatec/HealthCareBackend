using DomainModel.Entities.Users;

namespace DomainModel.Models.Users;
public class UserRegisterDto
{
    public string FullName { get; set; } = null!;

    //public string Username { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; } 


    public static implicit operator User(UserRegisterDto dto)
    {
        return  new User()
        {
            Id = Guid.NewGuid().ToString(),
            FullName = dto.FullName,
            Email = dto.Email?.ToLower(),
            //UserName = dto.Username,
            PhoneNumber = dto.PhoneNumber,

        };
    }
}

