using exam_system.Features.Identity.Login.DTOs.Internal;

namespace  exam_system.Common.Auth.Jwt;

public interface  ITokenService
{
    string GenerateAccessToken(AuthenticatedUserResult user);
    string GenerateRefreshToken();
}

