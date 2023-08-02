using DomainModel.Interfaces.Services;
using DomainModel.Models.Users;
using System.Diagnostics;

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

    [HttpPost]
    public async Task<IActionResult> Post(UserRegisterDto model)
    {
        var result = await authService.RegisterAsync(model);

        return Ok(result);
        //return Ok(authService.TestAuth());
    }
    





    [HttpGet("g1")]
    public async Task<IActionResult> Get1()
    {
        var stopWatch = Stopwatch.StartNew();
        int dateTimeR = 0 ;
        int dateDateTimeOffsetR =0;

        for (var i = 0; i < 100; i++)
        {
            stopWatch.Start();
            var d2 = DateTimeOffset.Now.UtcDateTime;
            stopWatch.Stop();
            dateDateTimeOffsetR = stopWatch.Elapsed.Microseconds;
        }
       
        var high = Stopwatch.IsHighResolution;
        return Ok(new {dateTimeR , dateDateTimeOffsetR, high });
    }
    
    [HttpGet("g2")]
    public async Task<IActionResult> Get2()
    {
        var stopWatch = Stopwatch.StartNew();
        int dateTimeR = 0 ;
        int dateDateTimeOffsetR =0;

        for (var i = 0; i<100;i++)
        {
            stopWatch.Start();
            var d1 = DateTime.Now.ToUniversalTime();
            stopWatch.Stop();
            dateTimeR = stopWatch.Elapsed.Microseconds;
        }
       
        var high = Stopwatch.IsHighResolution;
        return Ok(new {dateTimeR , dateDateTimeOffsetR, high });
    }
}
