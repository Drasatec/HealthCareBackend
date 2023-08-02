using Twilio.Rest.Api.V2010.Account;

namespace DomainModel.Interfaces.Services;

public interface ISMSService
{
    Task<MessageResource> Send(string mobileNumber, string body);
    Task<MessageResource?> SendVerificationCodeAsync(string mobileNumber, string? verificationCode, string? lang = null);
}
