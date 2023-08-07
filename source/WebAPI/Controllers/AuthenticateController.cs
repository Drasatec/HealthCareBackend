using DomainModel.Interfaces.Services;
using DomainModel.Models;
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
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.VerificationCode))
        {
            return BadRequest(new Response(false, "can not insert null or empty "));
        }

        var result = await authService.VerificationEmail(model.Email, model.VerificationCode);
        //var email = User.FindFirst("uid")?.Value;
        return Ok(result);
    }
    
    [HttpPost("send-verificationCode-email")]
    public async Task<IActionResult> SendVerificationCodeToEmail(UserVerificationEmailModel model)
    {
        var result = await authService.RenewEmailVerificationCode(model.Email);
        //var email = User.FindFirst("uid")?.Value;
        return Ok(result);
    }


    // SMS
    [HttpPost("send-verificationCode-sms")]
    public async Task<IActionResult> SendVerificationCodeToSms(UserVerificationPhoneModel model)
    {
        var result = await authService.RenewSmsVerificationCode(model.PhoneNumber);
        //var email = User.FindFirst("uid")?.Value;
        return Ok(result);
    }


    //[Authorize(Roles ="User")]
    [HttpPost("verification-sms")]
    public async Task<IActionResult> VerificationSMS(UserVerificationPhoneModel model)
    {
        //var userId = User.FindFirst("uid")?.Value;
        if(string.IsNullOrEmpty(model.PhoneNumber) ||string.IsNullOrEmpty(model.VerificationCode))
        {
            return BadRequest(new Response(false,"can not insert null or empty "));
        }
        var result = await authService.VerificationPhone(model.PhoneNumber,model.VerificationCode);
        return Ok(result);
    }


    //public async Task<IActionResult> ChangePassword(UserLoginDto model, [FromQuery] string verification = "email")
    //{

    //}






    // verification Email Code 
    // verification sms Code
    // role controlar
    // assign roles


    [HttpGet("g1")]
    public async Task<IActionResult> Get1()
    {
        string result = "";

        DateTimeOffset date1 = DateTimeOffset.UtcNow;
        DateTimeOffset date2 = DateTimeOffset.Parse("2023-08-07 20:30:43.767");

        // Compare if date1 is earlier than date2
        if (date1 < date2)
        {
            return Ok("1 date1 is earlier than date2");
        }

        // Compare if date1 is later than date2
        if (date1 > date2)
        {
            return Ok("2 date1 is later than date2");

        }

        // Compare if date1 is earlier than or equal to date2
        if (date1 <= date2)
        {
            return Ok("3 date1 is earlier than or equal to date2");

        }

        // Compare if date1 is later than or equal to date2
        if (date1 >= date2)
        {
            return Ok("4 date1 is later than or equal to date2");

        }




        DateTime expire = DateTime.Parse("2023-08-07 20:30:43.767");
        if (expire > DateTimeOffset.UtcNow)
            result = "expire > now";
        else if(expire == DateTimeOffset.UtcNow)
            result = "expire == now";
        else
            result = "now > expire";
           
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
