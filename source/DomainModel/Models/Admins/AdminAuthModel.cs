namespace DomainModel.Models.Admins;

public class AdminAuthModel
{
    public bool Success { get; set; }

    public string? Message { get; set; } = string.Empty;
    
    public string? AdminId { get; set; }

    public string? Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public List<string>? Roles { get; set; }

    public string? Token { get; set; } = string.Empty;

    public DateTime? ExpiresOn { get; set; }

    public AdminAuthModel(bool success, string? message)
    {
        Message = message;
        Success = success;
    }
    public AdminAuthModel()
    {

    }
}
