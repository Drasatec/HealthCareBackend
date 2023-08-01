using DomainModel.Interfaces.Services;
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

    public AuthService(IUnitOfWork data, IOptions<JWTSettings> jwt)
    {
        Jwt = jwt.Value;
        Data = data;
    }

    public string TestAuth()
    {
        return Data.Users.Test() + Jwt.Audience;
    }


    public async Task<AuthModel> RegisterAsync()
    {
        
        var model = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "username",
            Email = "email",
            FullName = "full name",
        };

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
