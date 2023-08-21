using DomainModel.Entities.SettingsEntities;
using DomainModel.Interfaces.Services;
using DomainModel.Models.Admins;
using DomainModel.Models.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Services;

public class AdminAuthService: IAdminAuthService
{
    private readonly JWTSettings Jwt;
    private readonly IUnitOfWork Data;
    private readonly IAdminRepository adminRepository;
    private readonly IMailingService mailingService;
    private readonly ISMSService smsService;
    private readonly IPasswordHasher passwordHasher;

    public AdminAuthService(IUnitOfWork data, IAdminRepository adminRepository, IMailingService mailingService, ISMSService smsService, IOptions<JWTSettings> jwt, IPasswordHasher passwordHasher)
    {
        Jwt = jwt.Value;
        Data = data;
        this.adminRepository = adminRepository;
        this.mailingService = mailingService;
        this.smsService = smsService;
        this.passwordHasher = passwordHasher;
    }
    public string TestAuth()
    {
        return adminRepository.Test() + Jwt.Audience;
    }

    public async Task<AdminAuthModel> LoginAsync(AdminLogin adminogin)
    {

        EmployeeAccount? employee;
        if (!string.IsNullOrEmpty(adminogin.UserName))
        {
            employee = await adminRepository.FindByUserNameAsync(adminogin.UserName);
        }
        else
            employee = null;


        if (employee is null || !await passwordHasher.VerifyPasswordAsync(adminogin.Password, employee.PasswordHash))
        {
            return new AdminAuthModel()
            {
                Message = "user name or Password is incorrect!",
                Success = false,
            };
        }

        var auth = new AdminAuthModel();

        var jwtSecurityToken = await CreateJwtToken(employee);

        auth.AdminId = employee.Id;
        auth.Email = employee.Email;
        auth.Username = employee.UserName;
        auth.Success = true;
        auth.Roles = new List<string> { "admin" };
        auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        auth.ExpiresOn = jwtSecurityToken.ValidTo;
        return auth;
    }

    //public async Task<AdminAuthModel> RegisterAsync(EmployeeAccountDto adminDto, string verification)
    //{

    //    // Ensure that the email does not already exist
    //    if (await adminRepository.IsUserNameExistAsync(adminDto.UserName))
    //    {
    //        return new AdminAuthModel(false, "userName is exist");
    //    }

    //    EmployeeAccount entity = adminDto;

    //    // insert new admin in database
    //    var user = await adminRepository.CreateAsync(entity, adminDto.Password);
    //    if (!user.Success)
    //    {
    //        return new AdminAuthModel(false, user?.Message);
    //    }

    //    //var jwtSecurityToken = await CreateJwtToken(entity);

    //    return new AdminAuthModel
    //    {
    //        AdminId = entity.Id,
    //        Success = true,
    //        Email = entity.Email,
    //        Roles = new List<string> { "admin" },
    //        Username = entity.UserName,
    //        ExpiresOn = null,
    //        Token = null,
    //    };
    //}

    private async Task<JwtSecurityToken> CreateJwtToken(EmployeeAccount admin)
    {
        // var userClaims = await _userManager.GetClaimsAsync(user);
        //var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        //foreach (var role in roles)
        //    roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(JwtRegisteredClaimNames.Email, admin.Email),
            new Claim("uid", admin.Id),
            new Claim("roles", "admin")
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
