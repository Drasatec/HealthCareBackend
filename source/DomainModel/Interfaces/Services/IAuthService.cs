using DomainModel.Entities.Users;
using DomainModel.Models;
using DomainModel.Models.Patients;
using DomainModel.Models.Users;
using DomainModel.Models.UsersV2;

namespace DomainModel.Interfaces.Services;
public interface IAuthService
{
    Task<Response> ChangePassword(UserChangePassDto dto);
    Task<Response<object>> DoesEmailExist(string email);
    Task<Response<object>> DoesPhoneExist(string email);
    Task<AuthModel> LoginAsync(UserLoginDto userDto);
    Task<AuthModel> RegisterAsync(UserRegisterDto userDto, string verification);
    Task<AuthModel> RegisterNewPatinetAsync(PatientRegisterDto userDto);
    Task<Response> RenewEmailVerificationCode(string email);
    Task<Response> RenewSmsVerificationCode(string phone);
    Task<Response<VerificationCodeModel>> SentVerificationCodeToEmail(string email);
    public string TestAuth();
    Task<AuthModel> UserLoginByEmailAsync(UserLoginByEmail userDto);
    Task<AuthModel> UserLoginByPhoneAsync(UserLoginByPhone userDto);
    Task<Response<PatientToLoginDto>> VerificationEmail(string email, string code);
    Task<Response<PatientToLoginDto>> VerificationPhone(string userId, string code);
}
