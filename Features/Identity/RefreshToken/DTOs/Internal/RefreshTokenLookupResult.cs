namespace exam_system.Features.Identity.RefreshToken.DTOs.Internal;

public record RefreshTokenLookupResult(
    Guid Id,
    Guid  UserId,
    bool IsUsed,
    bool IsRevoked,
    DateTime ExpiresAt
 
);
          