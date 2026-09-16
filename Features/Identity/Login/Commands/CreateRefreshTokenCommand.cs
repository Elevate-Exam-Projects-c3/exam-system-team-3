using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Login.Commands;

public record CreateRefreshTokenCommand(
    Guid UserId
) : IRequest<Result<string>>;