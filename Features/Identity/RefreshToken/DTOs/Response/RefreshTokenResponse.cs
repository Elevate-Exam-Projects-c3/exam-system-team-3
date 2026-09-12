namespace exam_system.Features.Identity.RefreshToken.DTOs.Response;

public record RefreshTokenResponse(
    string  AccessToken,
    string Role,
    Guid UserId
    );