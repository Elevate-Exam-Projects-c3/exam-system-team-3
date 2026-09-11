namespace exam_system.Features.Identity.VerifyEmailOtp.DTOs.Internal;

public record UserLookupResult(
    Guid Id,
    string Email,
    bool EmailConfirmed);