using SendGrid;
using SendGrid.Helpers.Mail;

namespace exam_system.Features.Shared.Email;

public class EmailService(IConfiguration config) : IEmailService
{
    public async Task SendOtpEmailAsync(string toEmail, string otp, CancellationToken cancellationToken = default)
    {
        var apiKey = config["SendGrid:ApiKey"];
        var fromEmail = config["SendGrid:FromEmail"];
        var fromName = config["SendGrid:FromName"] ?? "Exam System";

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress(toEmail);
        var subject = "Your Verification Code";
        var plainTextContent = $"Your OTP code is: {otp}. It expires in 10 minutes.";
        var htmlContent = $"<strong>Your OTP code is: {otp}</strong><br/>It expires in 10 minutes.";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg, cancellationToken);

        if ((int)response.StatusCode >= 400)
        {
            var body = await response.Body.ReadAsStringAsync(cancellationToken);
            throw new Exception($"SendGrid failed: {response.StatusCode} - {body}");
        }
    }
}