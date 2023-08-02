namespace DomainModel.Interfaces.Services;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    Task<bool> VerifyPasswordAsync(string password, string hashedPassword);
}
