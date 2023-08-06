namespace DomainModel.Models.Users;

public class AuthModel
{
    public string? UserId { get; set; } 

    
    public string Email { get; set; } = string.Empty;

    public bool IsAuthenticated { get; set; }

    public bool IsCompletData { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool EmailConfirmed { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Message { get; set; } = string.Empty;

    public List<string>? Roles { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresOn { get; set; }

    public AuthModel(string message)
    {
        Message = message;
    }
    public AuthModel()
    {
            
    }
}
