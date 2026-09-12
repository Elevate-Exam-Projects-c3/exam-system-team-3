using exam_system.Common.Enums;

namespace  exam_system.Common.Auth.Jwt;

public interface  ITokenService
{
    string GenerateAccessToken(Guid userId, string email, UserRole role);
    string GenerateRefreshToken();
}

