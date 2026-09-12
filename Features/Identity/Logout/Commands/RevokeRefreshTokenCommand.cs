using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Logout.Commands;

public record RevokeRefreshTokenCommand(
    string RawRefreshToken
) : IRequest<RequestResponse<bool>>;