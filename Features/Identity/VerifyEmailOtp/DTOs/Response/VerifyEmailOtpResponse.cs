namespace exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;

public record VerifyEmailOtpResponse(
    Guid UserId,
    string Email,
    string Message
);