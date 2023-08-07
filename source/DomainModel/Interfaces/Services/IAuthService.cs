using DomainModel.Models;
using DomainModel.Models.Users;

namespace DomainModel.Interfaces.Services;
public interface IAuthService
{
    Task<AuthModel> LoginAsync(UserLoginDto userDto);
    Task<AuthModel> RegisterAsync(UserRegisterDto userDto, string verification);
    Task<Response> RenewEmailVerificationCode(string email);
    Task<Response> RenewSmsVerificationCode(string phone);
    public string TestAuth();
    Task<Response> VerificationEmail(string email, string code);
    Task<Response> VerificationPhone(string userId, string code);
}
