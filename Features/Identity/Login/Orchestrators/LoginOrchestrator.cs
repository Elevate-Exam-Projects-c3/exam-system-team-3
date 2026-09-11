using exam_system.Features.Identity.Login.DTOs.Response;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Login.Orchestrators;


public record LoginCommand(
    string Email,
    string Password
) :IRequest<RequestResponse<LoginResponse>>;
