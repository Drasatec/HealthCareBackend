namespace DomainModel.Models.Users;

public class UserVerificationPhoneModel
{
    public string PhoneNumber { get; set; } = null!;
    public string VerificationCode { get; set; } = string.Empty;
    public DateTime? ExpirationTime { get; set; }
}
