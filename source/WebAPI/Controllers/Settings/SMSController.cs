using DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WebAPI.Controllers.Settings;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class SMSController : ControllerBase
{
    private readonly ISMSService _smsService;

    public SMSController(ISMSService smsService)
    {
        _smsService = smsService;
    }

    [HttpPost("send")]
    public IActionResult Mai([FromQuery] string mobileNumber, string body)
    {
        var accountSid = "AC207a5beb566133cc7513e9ae8abe9aca";
        var authToken = "b682bba886ddf54c8f710833dc5d7c38";
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
          new PhoneNumber("+201093564989"));
        messageOptions.From = new PhoneNumber("+17623005240");
        messageOptions.Body = body;

        var message = MessageResource.Create(messageOptions);
        Console.WriteLine(message.Body);

        return Ok(message.Body);
    }

    [HttpPost("send-fawzy")]
    public IActionResult Fawzy([FromQuery] string mobileNumber, string body)
    {
        var accountSid = "AC0ce3f78529c9f36a133057a43227a83b";
        var authToken = "57c63881e2fd00aacd18e8d488bfad15";
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
          new PhoneNumber("+201093052427"));
        messageOptions.From = new PhoneNumber("+12184323339");
        messageOptions.Body = "hi fawzy";


        var message = MessageResource.Create(messageOptions);
        Console.WriteLine(message.Body);

        return Ok(message.Body);
    }

}
