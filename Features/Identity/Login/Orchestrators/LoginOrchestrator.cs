using exam_system.Features.Identity.Login.DTOs.Response;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Login.Orchestrators;


public record LoginOrchestrator(
    string Email,
    string Password
) :IRequest<Result<LoginResponse>>;
