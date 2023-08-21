using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.Admins;
using DomainModel.Models.Users;

namespace WebAPI.Controllers;

[Route("api/AdminAuth")]
[ApiController]
[ApiVersion("1.0")]
public class AdminAuthController : ControllerBase
{
    private readonly IAdminRepository adminRepository;
    private readonly IAdminAuthService authService;
    private readonly IUnitOfWork Data;

    public AdminAuthController(IAdminRepository adminRepository,IAdminAuthService _adminAuthService, IUnitOfWork data)
    {
        this.adminRepository = adminRepository;
        authService = _adminAuthService;
        Data = data;
    }

    [HttpGet("t")]
    public async Task<IActionResult> Register()
    {
        return Ok(authService.TestAuth());
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateAnAdmin([FromBody] EmployeeAccountDto? model)
    {
        var username = await adminRepository.IsUserNameExistAsync(model.UserName);
        if (username)
        {
            return BadRequest(new Response(false, "user name is exist"));
        }

        var result = await adminRepository.CreateAsync(model);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AdminLogin model)
    {
        var result = await authService.LoginAsync(model);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
