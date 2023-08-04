using DomainModel.Interfaces.Services;
using DomainModel.Models.Users;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("reqister")]
    public async Task<IActionResult> Register(UserRegisterDto model, [FromQuery] string verification = "email")
    {
        var result = await authService.RegisterAsync(model, verification);


        return Ok(result);
        //return Ok(authService.TestAuth());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var result = await authService.LoginAsync(model);

        return Ok(result);
    }

    //Email
    [HttpPost("verification-email")]
    public async Task<IActionResult> VerificationEmail(UserVerificationEmailModel model)
    {
        var result = await authService.VerificationEmail(model.Email, model.VerificationCode);
        //var email = User.FindFirst("uid")?.Value;
        return Ok(result);
    }
    
    [HttpPost("send-verification-email")]
    public async Task<IActionResult> SendVerificationToEmail(UserVerificationEmailModel model)
    {
        var result = await authService.RenewEmailVerificationCode(model.Email);
        //var email = User.FindFirst("uid")?.Value;
        return Ok(result);
    }

    // SMS
    [Authorize(Roles ="User")]
    [HttpPost("verification-sms")]
    public async Task<IActionResult> VerificationSMS(UserVerificationEmailModel model)
    {
        var userId = User.FindFirst("uid")?.Value;
        if(userId == null)
        {
            return NoContent();
        }
        var result = await authService.VerificationPhone(userId, model.VerificationCode);
        return Ok(result);
    }
    

    // verification Email Code 
    // verification sms Code
    // role controlar
    // assign roles


    [HttpGet("g1")]
    public async Task<IActionResult> Get1()
    {
        bool result = false ;
        DateTime expire = DateTime.Parse("2023-08-03 13:29:41.623");
        if (expire > DateTimeOffset.UtcNow)
            result = true;
        return Ok(result);
    }

    [HttpGet("g2")]
    public async Task<IActionResult> Get2()
    {
        var stopWatch = Stopwatch.StartNew();
        int dateTimeR = 0;
        int dateDateTimeOffsetR = 0;

        for (var i = 0; i < 100; i++)
        {
            stopWatch.Start();
            var d1 = DateTime.Now.ToUniversalTime();
            stopWatch.Stop();
            dateTimeR = stopWatch.Elapsed.Microseconds;
        }

        var high = Stopwatch.IsHighResolution;
        return Ok(new { dateTimeR, dateDateTimeOffsetR, high });
    }
}
