using DomainModel.Helpers;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AppSettings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace WebAPI.Services;
public class SMSService : ISMSService
{
    private readonly TwilioSettings twilio;

    public SMSService(IOptions<TwilioSettings> twilio)
    {
        this.twilio = twilio.Value;
    }

    public async Task<MessageResource> Send(string mobileNumber, string body)
    {
        TwilioClient.Init(twilio.AccountSID, twilio.AuthToken);

        var result = await MessageResource.CreateAsync(
                body: body,
                from: new Twilio.Types.PhoneNumber(twilio.TwilioPhoneNumber),
                to: mobileNumber
            );

        return result;
    }

    public async Task<MessageResource?> SendVerificationCodeAsync(string mobileNumber, string? verificationCode, string? lang = null)
    {
        if(string.IsNullOrEmpty(mobileNumber))
        {
            return null;
        }
        if (verificationCode is null)
        {
            verificationCode = Helper.VerificationCode();
        }

        string smsMessage = $@"Verification Code: {verificationCode}
                        Use this code to verify your account. Please do not share it with anyone.
                        Thank you for using our service!";

        TwilioClient.Init(twilio.AccountSID,twilio.AuthToken);
        var messageOptions = new CreateMessageOptions(new PhoneNumber(mobileNumber))
        {
            From = new PhoneNumber(twilio.TwilioPhoneNumber),
            Body = smsMessage
        };

        var message = await MessageResource.CreateAsync(messageOptions);
        await Console.Out.WriteLineAsync(message.Body);
        await Console.Out.WriteLineAsync(mobileNumber);
        await Console.Out.WriteLineAsync(twilio.TwilioPhoneNumber);

        return message;
    }
}
