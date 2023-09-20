using DomainModel.Helpers;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AppSettings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using System.Net;
//using System.Net.Mail;
using MimeKit;

namespace WebAPI.Services;

public class MailingService : IMailingService
{
    private readonly MailSettings _mailSettings;

    public MailingService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string mailTo, string subject, string body, IList<Stream>? attachments = null)
    {
        await SendEmail(mailTo, subject, body, attachments);
    }

    public async Task SendEmailAsync(string mailTo, string subject, string body)
    {
        await SendEmail(mailTo, subject, body);
    }

    public async Task<string?> SendVerificationCodeAsync(string mailTo, string? verificationCode, string? userFullName = "", string? lang = null)
    {
        if (string.IsNullOrEmpty(mailTo))
        {
            return null;
        }
        if (verificationCode is null)
        {
            verificationCode = Helper.VerificationCode();
        }
        if (userFullName == null)
            userFullName = "";

        string subject = "Email Verification Code - Please Confirm Your Account";


        string htmlBody = $@"<html>
                                <head>
                                    <style>
                                        p {{
                                            font-size: 16px; 
                                        }}
                                        .verification-code {{
                                            font-size: 25px;
                                            font-weight: bold;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <p>Dear {userFullName},</p>

                                    <p>Thank you for registering with our service! To complete your account registration and ensure the security of your account, we require you to verify your email address.</p>

                                    <p><strong>Verification Code: <span class='verification-code'>{verificationCode}</span></strong></p>

                                    <p>To verify your email address, please follow these steps:</p>
                                    <ol>
                                        <li>Open our website/app and log in to your account.</li>
                                        <li>Go to the verification page or click on the verification link provided.</li>
                                        <li>Enter the above verification code when prompted.</li>
                                    </ol>

                                    <p>If you did not initiate this registration, please disregard this email.</p>

                                    <p>Thank you for being a part of our community!</p>

                                    <p>Best regards,</p>
                                    <p>Drasat Team</p>
                                </body>
                                </html>";

        string body = $@"Dear {userFullName},

                        Thank you for registering with our service! To complete your account registration and ensure the security of your account, we require you to verify your email address.

                        *Verification Code*: {verificationCode}

                        To verify your email address, please follow these steps:
                        1. Open our website/app and log in to your account.
                        2. Go to the verification page or click on the verification link provided.
                        3. Enter the above verification code when prompted.

                        If you did not initiate this registration, please disregard this email.

                        Thank you for being a part of our community!

                        Best regards,
                        Drasat Team";

        await SendEmail(mailTo, subject, htmlBody);
        return verificationCode;
    }

    private async Task SendEmail(string mailTo, string subject, string body)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Email),
            Subject = subject
        };

        email.To.Add(MailboxAddress.Parse(mailTo));

        var builder = new BodyBuilder();

        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port,true);
        smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);

    }



    private async Task SendEmail(string mailTo, string subject, string body, IList<Stream>? attachments)
    {
        //var email = new MimeMessage
        //{
        //    Sender = MailboxAddress.Parse(_mailSettings.Email),
        //    Subject = subject
        //};

        //email.To.Add(MailboxAddress.Parse(mailTo));

        //var builder = new BodyBuilder();

        ////if (attachments != null)
        ////{
        ////    byte[] fileBytes;
        ////    foreach (var file in attachments)
        ////    {
        ////        if (file.Length > 0)
        ////        {
        ////            using var ms = new MemoryStream();
        ////            file.CopyTo(ms);
        ////            fileBytes = ms.ToArray();

        ////            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        ////        }
        ////    }
        ////}

        //builder.HtmlBody = body;
        //email.Body = builder.ToMessageBody();
        //email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

        //using var smtp = new SmtpClient();
        //smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
        //await smtp.SendAsync(email);
        //smtp.Disconnect(true);
    }
}