using DomainModel.Entities.Users;
using DomainModel.Helpers;
using DomainModel.Interfaces.Services;
using DomainModel.Models;
using DomainModel.Models.Users;
using System.Diagnostics;
using System.Linq.Expressions;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IUnitOfWork Data;

    public AuthenticateController(IAuthService _authService, IUnitOfWork data)
    {
        authService = _authService;
        Data = data;
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
        if (string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.VerificationCode))
        {
            return BadRequest(new Response(false, "can not insert null or empty "));
        }
        var result = await authService.VerificationPhone(model.PhoneNumber, model.VerificationCode);
        return Ok(result);
    }



    [HttpGet("AllUsers", Order = 0111)]
    public async Task<IActionResult> GetAllNames([FromQuery] string? lang, bool? active, int page = 1, int pageSize = Constants.PageSize)
    {

        Expression<Func<User, object>> order = o => o.CreateOn;


        var result = await Data.Generic.GenericSelectionReadAll<User, object>(null, (user) =>
        new
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            CreateAt = user.CreateOn
            
        },
        order,
        page, pageSize);
        return Ok(result);
    }



    [HttpDelete("delete/{phoneNumber?}", Order = 0830)]
    public async Task<IActionResult> Delete(string? phoneNumber, string? email)
    {
        Response res = new Response();

        if (phoneNumber != null)
        {
            res = await Data.Generic.GenericDelete<User>(t => t.PhoneNumber == phoneNumber);
        }
        if (email != null)
        {
            res = await Data.Generic.GenericDelete<User>(t => t.Email == email);
        }

        if (res.Success)
            return Ok(res);
        return BadRequest(res);
    }


    //public async Task<IActionResult> ChangePassword(UserLoginDto model, [FromQuery] string verification = "email")
    //{

    //}


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
