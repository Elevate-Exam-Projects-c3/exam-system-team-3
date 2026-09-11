using exam_system.Features.Identity.Login.DTOs.Internal;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Login.Commands;

public record AuthenticateUserCommand(
    string Email,
    string Password
) : IRequest<RequestResponse<AuthenticatedUserResult>>;