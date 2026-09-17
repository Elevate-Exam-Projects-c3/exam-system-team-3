namespace exam_system.Features.Identity.ForgotPassword.DTOs.Internal;

public record PasswordResetOtpLookupResult(
    Guid Id,
    Guid UserId,
    string OtpHash,
    int AttemptCount,
    DateTime ExpiresAt
);