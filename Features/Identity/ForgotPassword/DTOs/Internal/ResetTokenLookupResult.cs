namespace exam_system.Features.Identity.ForgotPassword.DTOs.Internal;

public record ResetTokenLookupResult(
    Guid Id,
    Guid UserId,
    DateTime? ResetTokenExpiresAt
);