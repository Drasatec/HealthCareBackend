using DomainModel.Interfaces.Services;
using DomainModel.Models.AppSettings;
using DomainModel.Models.Users;
using Microsoft.AspNetCore.Identity;
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
    private readonly IPasswordHasher passwordHasher;

    public AuthService(IUnitOfWork data, IUserRepository userRepository, IOptions<JWTSettings> jwt, IPasswordHasher passwordHasher)
    {
        Jwt = jwt.Value;
        Data = data;
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
    }

    public string TestAuth()
    {
        return userRepository.Test() + Jwt.Audience;
    }


    public async Task<AuthModel> RegisterAsync(UserRegisterDto userDto)
    {
        var user = await userRepository.CreateAsync(userDto);

        if (!user.Success)
        {
            return new AuthModel { Message = user?.Message };
        }

        ApplicationUser model = new ApplicationUser();
        if (user.Value != null)
        {
            model.Id = Guid.NewGuid().ToString();
            model.FullName = user.Value.FullName;
            model.UserName = user.Value.Username;
            model.Email = user.Value.Email;
        }


        var jwtSecurityToken = await CreateJwtToken(model);

        return new AuthModel
        {
            Email = model.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = new List<string> { "User" },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = model.UserName
        };
    }


    public async Task<AuthModel> LoginAsync(UserRegisterDto userDto)
    {
        var authModel = new AuthModel();

        var user = await userRepository.FindByEmailAsync(userDto.Email);

        if (user is null || !await passwordHasher.VerifyPasswordAsync(userDto.Password, user.PasswordHash))
        {
            authModel.Message = "Email or Password is incorrect!";
            return authModel;
        }

        ApplicationUser model = new ApplicationUser();
        if (user != null)
        {
            model.Id = user.Id;
            model.FullName = user.FullName;
            model.UserName = user.UserName;
            model.Email = user.Email;
        }

        var jwtSecurityToken = await CreateJwtToken(model);
        //var rolesList = await _userManager.GetRolesAsync(user);


        return new AuthModel
        {
            Email = model.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = new List<string> { "User", "Patient" },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = model.UserName
        };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
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
