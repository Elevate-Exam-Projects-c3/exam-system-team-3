using exam_system.Common.Enums;

namespace exam_system.Features.Identity.Login.DTOs.Internal;

public record AuthenticatedUserResult(
    Guid UserId,
    string Email,
    UserRole Role
);