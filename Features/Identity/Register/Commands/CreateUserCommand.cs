using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Identity.Register.Commands;

public record CreateUserCommand(
    string FullName, 
    string Email, 
    string Password) : IRequest<Result<Guid>>;