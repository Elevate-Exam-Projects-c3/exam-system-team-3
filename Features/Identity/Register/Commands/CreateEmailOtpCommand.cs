namespace exam_system.Features.Identity.Register.Commands;

public record CreateEmailOtpCommand(
    Guid UserId, 
    string Email) : IRequest<string>;