namespace exam_system.Features.Shared.Email;

public interface IEmailService
{
    Task SendOtpEmailAsync(string toEmail, string otp, CancellationToken cancellationToken = default);
}