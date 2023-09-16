using DomainModel.Interfaces.Services;
using DomainModel.Models.AppSettings;
using DomainModel.Models.Mails;
using System.Net.Mail;
using System.Net;
using Twilio.TwiML.Messaging;
using MailKit.Security;
using MimeKit;

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
        await _mailingService.SendVerificationCodeAsync("mofawzyhelal@gmail.com","000000");
        return Ok(dto);
    }

    //public async Task<IActionResult> ttt()
    //{
    //    // Sender's email and password
    //    string senderEmail = "customersservice@alrahmavip.com";
    //    string senderPassword = "Aa22.#33Aa";

    //    var email = new MimeMessage
    //    {
    //        Sender = MailboxAddress.Parse(senderEmail),
    //        Subject = "Subject"
    //    };

    //    email.To.Add(MailboxAddress.Parse("mofawzyhelal@gmail.com"));

    //    var builder = new BodyBuilder();

    //    builder.TextBody = "text body body";
    //    email.Body = builder.ToMessageBody();
    //    email.From.Add(new MailboxAddress("Drasate", senderEmail));

    //    try
    //    {
    //        using var smtp = new SmtpClient();
    //        smtp.Connect("mail.alrahmavip.com", 25, SecureSocketOptions.StartTls);
    //        smtp.Authenticate(senderEmail, senderPassword);
    //        await smtp.SendAsync(email);
    //        smtp.Disconnect(true);
    //        return Ok("Email sent successfully.");
    //    }
    //    catch (Exception ex)
    //    {
    //        return Ok($"Error sending email: {ex.Message}, {ex.InnerException?.Message}");

    //    }
    //}
}
