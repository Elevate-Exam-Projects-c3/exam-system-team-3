using exam_system.Features.Identity.RefreshToken.DTOs.Internal;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.RefreshToken.Queries;

public record GetUserByIdQuery
(
    Guid UserId
    ) : IRequest<RequestResponse<RefreshedUserResult>>;