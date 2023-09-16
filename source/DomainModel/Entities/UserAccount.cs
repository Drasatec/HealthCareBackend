namespace DomainModel.Entities;

public class UserAccount
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CallingCode { get; set; }

    public string? Email { get; set; } 

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? VerificationCode { get; set; }

    public DateTimeOffset? ExpirationTime { get; set; }

    public string PasswordHash { get; set; } = null!;

    public byte UserType { get; set; }

    public int UserReferenceId { get; set; }

    public virtual Patient UserReference { get; set; } = null!;
}
