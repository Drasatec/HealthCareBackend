using DomainModel.Models.Users;

namespace DomainModel.Interfaces.Services;
public interface IAuthService
{
    Task<AuthModel> RegisterAsync(UserRegisterDto userDto);
    public string TestAuth();
}
