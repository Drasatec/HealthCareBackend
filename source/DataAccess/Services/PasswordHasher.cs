using DomainModel.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convert the password string to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // Compute the hash value of the password bytes
            byte[] hashBytes = sha256Hash.ComputeHash(bytes);

            // Convert the hash bytes to a string representation
            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convert the password string to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // Compute the hash value of the password bytes
            byte[] hashBytes = sha256Hash.ComputeHash(bytes);

            // Convert the hash bytes to a string representation
            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            // Compare the generated hash with the stored hashed password
            return builder.ToString() == hashedPassword;
        }
    }

    public async Task<bool> VerifyPasswordAsync(string password, string hashedPassword)
    {
        return await Task.FromResult(VerifyPassword(password, hashedPassword));
    }
}