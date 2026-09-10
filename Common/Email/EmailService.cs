// Features/Shared/Email/EmailService.cs
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace exam_system.Common.Email;
public class EmailService(
    ISendGridClient sendGridClient, 
    IOptions<SendGridOptions> options) : IEmailService
{
    private readonly SendGridOptions _options = options.Value;

    public async Task SendOtpEmailAsync(string toEmail, string otp, CancellationToken cancellationToken = default)
    {
        var from = new EmailAddress(_options.FromEmail, _options.FromName);
        var to = new EmailAddress(toEmail);
        var subject = "Your Verification Code";
        var plainTextContent = $"Your OTP code is: {otp}";
        var htmlContent = $"<strong>Your OTP code is: {otp}</strong>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        await sendGridClient.SendEmailAsync(msg, cancellationToken);
    }
}