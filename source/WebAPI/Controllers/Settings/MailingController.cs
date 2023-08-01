using DomainModel.Interfaces.Services;
using DomainModel.Models.Mails;

namespace WebAPI.Controllers.Settings;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class MailingController : ControllerBase
{
    private readonly IMailingService _mailingService;

    public MailingController(IMailingService mailingService)
    {
        _mailingService = mailingService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMail(IFormFileCollection files, [FromForm] MailRequestDto dto)
    {
        await _mailingService.SendEmailAsync(dto.ToEmail, dto.Subject, dto.Body, null!);
        return Ok();
    }

    //[HttpPost("welcome")]
    //public async Task<IActionResult> SendWelcomeEmail([FromBody] WelcomeRequestDto dto)
    //{
    //    var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";
    //    var str = new StreamReader(filePath);

    //    var mailText = str.ReadToEnd();
    //    str.Close();

    //    mailText = mailText.Replace("[username]", dto.UserName).Replace("[email]", dto.Email);

    //    await _mailingService.SendEmailAsync(dto.Email, "Welcome to our channel", mailText);
    //    return Ok();
    //}
}
