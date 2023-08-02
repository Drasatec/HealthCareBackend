namespace DomainModel.Interfaces.Services;

public interface IMailingService
{
    Task SendEmailAsync(string mailTo, string subject, string body, IList<Stream>? attachments = null);
    Task<string?> SendVerificationCodeAsync(string mailTo, string? verificationCode, string userFullName = "", string? lang = null);
}
