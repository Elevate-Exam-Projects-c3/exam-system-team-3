using exam_system.Features.Identity.Register.DTOs.Response;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Register.Orchestrators;

public record RegisterOrchestrator(
    string FullName,
    string Email,
    string Password
) : IRequest<Result<RegisterUserResponse>>;