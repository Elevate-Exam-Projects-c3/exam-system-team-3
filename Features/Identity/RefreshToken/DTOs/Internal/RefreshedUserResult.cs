using exam_system.Common.Enums;

namespace exam_system.Features.Identity.RefreshToken.DTOs.Internal;

public record RefreshedUserResult(
    Guid UserId,
    string Email,
    UserRole Role
);