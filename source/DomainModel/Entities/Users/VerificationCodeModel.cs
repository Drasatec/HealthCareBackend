namespace DomainModel.Entities.Users;
public class VerificationCodeModel
{
    public string Id { get; set; } = new Guid().ToString();

    public string VerificationBy { get; set; } = null!;

    public string? VerificationCode { get; set; }

    public DateTimeOffset? ExpirationTime { get; set; }

}
