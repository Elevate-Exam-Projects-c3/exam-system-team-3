namespace exam_system.Features.Identity.Login.DTOs.Request;

public record LoginRequest(
    string Email,
    string Password
);