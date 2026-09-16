using exam_system.Features.Identity.RefreshToken.DTOs.Response;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.RefreshToken.Orchestrators;

public record RefreshTokenOrchestrator(
    string  RawRefreshToken
    ):IRequest<Result<RefreshTokenResponse>>;