
namespace exam_system.Features.Identity.VerifyEmailOtp.DTOs.Request;

public record VerifyEmailOtpRequest(
    string Email,
    string Otp
) ;