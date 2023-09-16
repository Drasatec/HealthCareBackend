using DomainModel.Entities.Users;
using DomainModel.Helpers;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.AppSettings;
using DomainModel.Models.Patients;
using DomainModel.Models.Users;
using DomainModel.Models.UsersV2;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
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
                await mailingService.SendVerificationCodeAsync(entity.Email, entity.VerificationCode, entity.FullName);

            }
            else if (verification.Equals("sms") && entity.PhoneNumber != null)
            {
                entity.VerificationCode = Helper.VerificationCode();
                await smsService.SendVerificationCodeAsync(entity.PhoneNumber, entity.VerificationCode);
            }
        }

        // insert new user in database
        var user = await userRepository.CreateAsync(entity, userDto.Password);
        if (!user.Success)
        {
            return new AuthModel(false, user?.Message);
        }

        //var jwtSecurityToken = await CreateJwtToken(entity);

        return new AuthModel
        {
            //UserId = entity.Id,
            //Success = true,
            //Email = entity.Email,
            //Roles = new List<string> { "User" },
            ////Username = entity.UserName,
            //IsAuthenticated = false,
            //ExpiresOn = null,
            //Token = null,
        };
    }


    public async Task<AuthModel> RegisterNewPatinetAsync(PatientRegisterDto userDto, string verification)
    {
        // Ensure that the email does not already exist
        if (!string.IsNullOrEmpty(userDto.Email) && await userRepository.IsEmailExistAsync(userDto.Email))
        {
            return new AuthModel(false, "email is exist");
        }

        // Ensure that the Phone Number does not already exist
        if (!string.IsNullOrEmpty(userDto.PhoneNumber) && await userRepository.IsPhoneExistAsync(userDto.PhoneNumber))
        {
            return new AuthModel(false, "Phone Number is exist");
        }

        Patient entity = userDto;
        UserAccount userAccount = new UserAccount()
        {
            UserName = "User"+ DateTime.Now.ToString("yyyyMMddHHmmss"),
            PhoneNumber = userDto.PhoneNumber,
            CallingCode = userDto.CallingCode,
            Email = userDto.Email,
        };

        if (!string.IsNullOrEmpty(verification))
        {
            if (verification.Equals("email"))
            {

                userAccount.VerificationCode = Helper.VerificationCode();
                _ = mailingService.SendVerificationCodeAsync(userAccount.Email!, userAccount.VerificationCode, userDto.FullName);
            }
            else if (verification.Equals("sms") && userAccount.PhoneNumber != null)
            {

                userAccount.VerificationCode = Helper.VerificationCode();
                _ = smsService.SendVerificationCodeAsync(userAccount.PhoneNumber, userAccount.VerificationCode);
            }
        }
        entity.UserAccount = userAccount;
        // insert new user in database
        var user = await userRepository.CreateWithNewPatientAsync(entity, userDto.Password);
        if (!user.Success)
        {
            return new AuthModel(false, user?.Message);
        }

        return await CreateAuth(userAccount);
    }

    public async Task<AuthModel> UserLoginByEmailAsync(UserLoginByEmail userDto)
    {
        UserAccount? user;
        if (!string.IsNullOrEmpty(userDto.Email))
        {
            user = await userRepository.FindByEmailAsync(userDto.Email);
            if (user is not null && await passwordHasher.VerifyPasswordAsync(userDto.Password, user.PasswordHash))
            {
                return await CreateAuth(user);
            }
        }
        return new AuthModel()
        {
            Message = "Email or Password is incorrect!",
            Success = false,
        };
    }

    public async Task<AuthModel> UserLoginByPhoneAsync(UserLoginByPhone userDto)
    {
        UserAccount? user;
        if (!string.IsNullOrEmpty(userDto.PhoneNumber))
        {
            user = await userRepository.FindByPhoneNumberAsync(userDto.PhoneNumber);
            if (user is not null && await passwordHasher.VerifyPasswordAsync(userDto.Password, user.PasswordHash))
            {
                return await CreateAuth(user);
            }
        }
        return new AuthModel()
        {
            Message = "PhoneNumber or Password is incorrect!",
            Success = false,
        };

    }

    public async Task<Response<PatientToLoginDto>> VerificationEmail(string email, string code)
    {
        //Response<PatientToLoginDto> res;
        var user = await userRepository.ReadUserIdByEmailAsync(email);
        // if expierd?

        if (user != null)
        {
            if (user.ExpirationTime >= DateTimeOffset.UtcNow && user.VerificationCode.Equals(code))
            {
                user.EmailConfirmed = true;
                var result = await userRepository.GenericUpdateSinglePropertyById(0, user, p => p.EmailConfirmed);
                if (result != null && result.Success)
                {
                    return new Response<PatientToLoginDto>(true, null, new PatientToLoginDto { UserId = user.Id, PatientId = null });

                }
                else
                    return new Response<PatientToLoginDto>(false, result?.Message, null);
            }
            else
                return new Response<PatientToLoginDto>(false, "Expired Time", null);
        }
        return new Response<PatientToLoginDto>(false, "user not found", null);
    }


    public async Task<Response<PatientToLoginDto>> VerificationPhone(string phoneNumber, string code)
    {
        Response<PatientToLoginDto> res;
        var user = await userRepository.ReadUserIdByPhoneAsync(phoneNumber);

        if (user != null)
        {
            if (user.ExpirationTime >= DateTimeOffset.UtcNow && user.VerificationCode.Equals(code))
            {
                user.PhoneNumberConfirmed = true;
                var result = await userRepository.GenericUpdateSinglePropertyById(0, user, p => p.PhoneNumberConfirmed);
                if (result != null && result.Success)
                {
                    return new Response<PatientToLoginDto>(true, null, new PatientToLoginDto { UserId = user.Id, PatientId = null });
                }
                else
                    return new Response<PatientToLoginDto>(false, result?.Message, null);
            }
            else
                return new Response<PatientToLoginDto>(false, "Expired Time", null);
        }
        return res = new Response<PatientToLoginDto>(false, "user not found", null);
    }



    public async Task<Response> RenewEmailVerificationCode(string email)
    {
        Response res;
        try
        {
            var user = await userRepository.ReadUserIdByEmailAsync(email);
            if (user != null)
            {
                user.VerificationCode = Helper.VerificationCode();
                await mailingService.SendVerificationCodeAsync(email, user.VerificationCode, user.FullName);
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
        catch (Exception ex)
        {
            return new Response(false, ex.Message);
        }
    }


    public async Task<Response> RenewSmsVerificationCode(string phone)
    {
        Response res;
        var user = await userRepository.ReadUserIdByPhoneAsync(phone);
        if (user != null)
        {
            user.VerificationCode = Helper.VerificationCode();
            await smsService.SendVerificationCodeAsync(phone, user.VerificationCode);

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


    public async Task<Response> ChangePassword(UserChangePassDto dto)
    {
        Response res;

        //UserAccount? user;
        //if (!string.IsNullOrEmpty(dto.Email))
        //{
        //    user = await userRepository.FindByEmailAsync(dto.Email);
        //}
        //else if (!string.IsNullOrEmpty(dto.UserId))
        //{
        //    user = await userRepository.FindById(dto.UserId);
        //}
        //else
        //    user = null;

        //if (user != null)
        //{
        //    var oldPass = passwordHasher.VerifyPassword(dto.OldPassword, user.PasswordHash);
        //    if (oldPass)
        //    {
        //        user.PasswordHash = passwordHasher.HashPassword(dto.Password);
        //        res = await userRepository.GenericUpdateSinglePropertyById(0, user, p => p.PasswordHash);
        //        res.Message = null;
        //        return res;
        //    }
        //    else
        //        return new Response(false, "Old password is wrong.");
        //}
        return res = new Response(false, "email or id is not founded");
    }


    private async Task<AuthModel> CreateAuth(UserAccount user)
    {
        var auth = new AuthModel();
        var userAccount = new UserAccountDto()
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.CallingCode + user.PhoneNumber,
            UserType = UserType.Patient.ToString(),

            EmailConfirmed = user.EmailConfirmed ? true : false,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed ? true : false,

        };
        auth.Success = true;
        auth.UserAccount = userAccount;
        var jwtSecurityToken = await CreateJwtToken(user);
        auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        auth.ExpiresOn = jwtSecurityToken.ValidTo;
        return auth;
    }

    private async Task<JwtSecurityToken> CreateJwtToken(UserAccount user)
    {
        // var userClaims = await _userManager.GetClaimsAsync(user);
        //var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        //foreach (var role in roles)
        //    roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim("uid", user.Id.ToString()),
            new Claim("roles", "patient"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName?? ""),
            new Claim(JwtRegisteredClaimNames.Email, user.Email??"")
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


    // =======================deleted

    public async Task<AuthModel> LoginAsync(UserLoginDto userDto)
    {

        //User? user;
        //if (!string.IsNullOrEmpty(userDto.Email))
        //{
        //    user = await userRepository.FindByEmailAsync(userDto.Email);
        //}
        //else if (!string.IsNullOrEmpty(userDto.PhoneNumber))
        //{
        //    user = await userRepository.FindByPhoneNumberAsync(userDto.PhoneNumber);
        //}
        //else
        //    user = null;


        //if (user is null || !await passwordHasher.VerifyPasswordAsync(userDto.Password, user.PasswordHash))
        //{
        //    return new AuthModel()
        //    {
        //        Message = "Email or Phone Number or Password is incorrect!",
        //        Success = false,
        //    };
        //}

        var auth = new AuthModel();
        return auth;
    }
    //var claims = new Claim[4];
    //claims[0] = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
    //claims[0] = new Claim("uid", user.Id.ToString());
    //claims[0] = new Claim("roles", "Patient");

    //    if (!string.IsNullOrEmpty(user.Email))
    //        claims[0] = new Claim(JwtRegisteredClaimNames.Email, user.Email);
    //    if (!string.IsNullOrEmpty(user.UserName))
    //        claims[0] = new Claim(JwtRegisteredClaimNames.Sub, user.UserName);

}
