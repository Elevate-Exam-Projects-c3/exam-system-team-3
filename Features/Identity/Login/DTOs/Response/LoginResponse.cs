using exam_system.Common.Enums;

namespace exam_system.Features.Identity.Login.DTOs.Response;


public record LoginResponse(
    string AccessToken,
    string Role,
    Guid  UserId
    );