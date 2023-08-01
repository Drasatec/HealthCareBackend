namespace DomainModel.Interfaces.Services;

public interface IMailingService
{
    Task SendEmailAsync(string mailTo, string subject, string body, IList<Stream> attachments = null);
}
