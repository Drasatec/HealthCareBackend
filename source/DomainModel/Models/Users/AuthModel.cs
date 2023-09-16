using DomainModel.Models.UsersV2;

namespace DomainModel.Models.Users;

public class AuthModel
{
    public bool Success { get; set; }

    public string? Message { get; set; } = string.Empty;

    public UserAccountDto? UserAccount { get; set; }

    public string? Token { get; set; } = string.Empty;

    public DateTime? ExpiresOn { get; set; }


    //public List<string>? Roles { get; set; }

    public AuthModel(bool success, string? message)
    {
        Message = message;
        Success = success;
    }
    public AuthModel()
    {
            
    }
}
