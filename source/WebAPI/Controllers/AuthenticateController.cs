using DomainModel.Interfaces.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthenticateController(IAuthService _authService)
    {
        authService = _authService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await authService.RegisterAsync();

        return Ok(result);
        //return Ok(authService.TestAuth());
    }
}
