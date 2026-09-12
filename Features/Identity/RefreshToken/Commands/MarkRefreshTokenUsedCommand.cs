using exam_system.Features.Shared;

namespace exam_system.Features.Identity.RefreshToken.Commands;

public record MarkRefreshTokenUsedCommand(
    Guid TokenId
    ): IRequest<RequestResponse<bool>>;
