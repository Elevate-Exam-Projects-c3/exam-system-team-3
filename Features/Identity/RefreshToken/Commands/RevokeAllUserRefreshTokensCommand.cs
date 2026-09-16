using exam_system.Features.Shared;

namespace exam_system.Features.Identity.RefreshToken.Commands;

public record RevokeAllUserRefreshTokensCommand(
    Guid UserId
    ): IRequest<Result<bool>>;