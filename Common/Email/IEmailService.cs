namespace exam_system.Common.Email;
public interface IEmailService
{
    Task SendOtpEmailAsync(string toEmail, string otp, CancellationToken cancellationToken = default);
}