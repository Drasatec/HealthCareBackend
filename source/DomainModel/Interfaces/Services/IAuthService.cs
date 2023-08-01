using DomainModel.Models.Users;

namespace DomainModel.Interfaces.Services;
public interface IAuthService
{
    Task<AuthModel> RegisterAsync();
    public string TestAuth();
}
