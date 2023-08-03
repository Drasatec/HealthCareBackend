namespace DomainModel.Models.Users;

public class UserVerificationEmailModel
{
    public string Email { get; set; } = null!;
    public string VerificationCode { get; set; } = string.Empty;
    public DateTime? ExpirationTime { get; set; }
}
