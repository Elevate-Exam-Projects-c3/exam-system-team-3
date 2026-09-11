namespace exam_system.Features.Identity.VerifyEmailOtp.DTOs.Internal;

public record OtpLookupResult(
    Guid Id,
    string OtpHash,
    int AttemptCount,
    DateTime ExpiresAt);