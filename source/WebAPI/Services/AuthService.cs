using DomainModel.Entities.Users;
using DomainModel.Helpers;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.AppSettings;
using DomainModel.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly JWTSettings Jwt;
    private readonly IUnitOfWork Data;
    private readonly IUserRepository userRepository;
    private readonly IMailingService mailingService;
    private readonly ISMSService smsService;
    private readonly IPasswordHasher passwordHasher;

    public AuthService(IUnitOfWork data, IUserRepository userRepository, IMailingService mailingService, ISMSService smsService, IOptions<JWTSettings> jwt, IPasswordHasher passwordHasher)
    {
        Jwt = jwt.Value;
        Data = data;
        this.userRepository = userRepository;
        this.mailingService = mailingService;
        this.smsService = smsService;
        this.passwordHasher = passwordHasher;
    }

    public string TestAuth()
    {
        return userRepository.Test() + Jwt.Audience;
    }

    public async Task<AuthModel> RegisterAsync(UserRegisterDto userDto, string verification)
    {

        // Ensure that the email does not already exist
        if (await userRepository.IsEmailExistAsync(userDto.Email))
        {
            return new AuthModel(false, "email is exist");
        }

        // Ensure that the Phone Number does not already exist
        if (await userRepository.IsPhoneExistAsync(userDto.PhoneNumber))
        {
            return new AuthModel(false, "Phone Number is exist");
        }

        User entity = userDto;

        // Send the verification code to the mail or phone
        if (!string.IsNullOrEmpty(verification))
        {
            if (verification.Equals("email"))
            {
                entity.VerificationCode = Helper.VerificationCode();
                _ = mailingService.SendVerificationCodeAsync(entity.Email, entity.VerificationCode, entity.FullName);
            }
            else if (verification.Equals("sms") && entity.PhoneNumber != null)
            {
                entity.VerificationCode = Helper.VerificationCode();
                _ = smsService.SendVerificationCodeAsync(entity.PhoneNumber, entity.VerificationCode);
            }
        }

        // insert new user in database
        var user = await userRepository.CreateAsync(entity, userDto.Password);
        if (!user.Success)
        {
            return new AuthModel(false, user?.Message);
        }

        var jwtSecurityToken = await CreateJwtToken(entity);

        return new AuthModel
        {
            Email = entity.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Success = true,
            Roles = new List<string> { "User" },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = entity.UserName
        };
    }


    public async Task<AuthModel> LoginAsync(UserLoginDto userDto)
    {
        var authModel = new AuthModel();

        var user = await userRepository.FindByEmailAsync(userDto.Email);

        if (user is null || !await passwordHasher.VerifyPasswordAsync(userDto.Password, user.PasswordHash))
        {
            authModel.Message = "Email or Password is incorrect!";
            authModel.Success = false;
            return authModel;
        }
        var auth = new AuthModel();

        // IsCompletData?
        var userId = await Data.Patients.FindByUserId(user.Id);
        if (userId is not null)
        {
            auth.IsCompletData = true;
        }

        // isConfirmed? Email or phone
        auth.EmailConfirmed = user.EmailConfirmed ? true : false;
        auth.PhoneNumberConfirmed = user.PhoneNumberConfirmed ? true : false;

        var jwtSecurityToken = await CreateJwtToken(user);

        auth.UserId = user.Id;
        auth.Email = user.Email;
        auth.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Success = true;
        auth.IsAuthenticated = true;
        auth.Roles = new List<string> { "User", "Patient" };
        auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        auth.Username = user.UserName;
        return auth;
    }


    public async Task<Response> VerificationEmail(string email, string code)
    {
        Response res;
        var user = await userRepository.ReadUserIdByEmailAsync(email);
        // if expierd?

        if (user != null)
        {
            if (user.ExpirationTime >= DateTimeOffset.UtcNow && user.VerificationCode.Equals(code))
            {
                user.EmailConfirmed = true;
                res = await userRepository.GenericUpdateSinglePropertyById(0, user, p => p.EmailConfirmed);
                res.Message = null;
                return res;
            }
            else
                return new Response(false, "Expired Time");
        }
        return res = new Response(false, "error");
    }


    public async Task<Response> VerificationPhone(string phoneNumber, string code)
    {
        Response res;
        var user = await userRepository.ReadUserIdByPhoneAsync(phoneNumber);

        if (user != null)
        {
            if (user.ExpirationTime >= DateTimeOffset.UtcNow && user.VerificationCode.Equals(code))
            {
                user.PhoneNumberConfirmed = true;
                res = await userRepository.GenericUpdateSinglePropertyById(0, user, p => p.PhoneNumberConfirmed);
                res.Message = null;
                return res;
            }
            else
                return new Response(false, "Expired Time");
        }
        return res = new Response(false, "user not found");
    }



    public async Task<Response> RenewEmailVerificationCode(string email)
    {
        Response res;
        var user = await userRepository.ReadUserIdByEmailAsync(email);
        if (user != null)
        {
            user.VerificationCode = Helper.VerificationCode();
            _ = mailingService.SendVerificationCodeAsync(email, user.VerificationCode, user.FullName);
            var isUpdated = await userRepository.UpdateVerificationCode(user);
            if (isUpdated)
                res = new Response(true, null);
            else
                res = new Response(false, "Something went wrong with the database");
        }
        else
            res = new Response(false, "email not found");
        return res;
    }

    public async Task<Response> RenewSmsVerificationCode(string phone)
    {
        Response res;
        var user = await userRepository.ReadUserIdByPhoneAsync(phone);
        if (user != null)
        {
            user.VerificationCode = Helper.VerificationCode();
            _ = smsService.SendVerificationCodeAsync(phone, user.VerificationCode);

            var isUpdated = await userRepository.UpdateVerificationCode(user);
            if (isUpdated)
                res = new Response(true, null);
            else
                res = new Response(false, "Something went wrong with the database");
        }
        else
            res = new Response(false, "pnone number is not found not found");

        return res;
    }


    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        // var userClaims = await _userManager.GetClaimsAsync(user);
        //var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        //foreach (var role in roles)
        //    roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id),
            new Claim("roles", "User")
        };
        //.Union(userClaims)
        //.Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: Jwt.Issuer,
            audience: Jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(Jwt.DurationInDays),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}
