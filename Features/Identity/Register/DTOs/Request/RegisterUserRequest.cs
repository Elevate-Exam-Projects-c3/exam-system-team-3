namespace exam_system.Features.Identity.Register.DTOs.Request;

public record RegisterUserRequest(
    string FullName,
    string Email,
    string Password
);