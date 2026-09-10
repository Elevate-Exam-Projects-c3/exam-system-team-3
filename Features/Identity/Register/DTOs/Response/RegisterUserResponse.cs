namespace exam_system.Features.Identity.Register.DTOs.Response;

public record RegisterUserResponse(
    Guid UserId,
    string Email,
    string Message
);