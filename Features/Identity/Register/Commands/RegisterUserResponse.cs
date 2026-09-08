namespace exam_system.Features.Identity.Register.Commands;

public record RegisterUserResponse(
    Guid UserId,
    string Email,
    string Message
);