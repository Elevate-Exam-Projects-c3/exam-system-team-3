namespace exam_system.Features.Shared.Results.ErrorCodes;

public class RefreshTokenErrors
{
    public static readonly Error TokenNotFound =
        Error.Unauthorized("REFRESH_TOKEN_NOT_FOUND", "Invalid refresh token.");

    public static readonly Error TokenExpired =
        Error.Unauthorized("REFRESH_TOKEN_EXPIRED", "Refresh token has expired. Please log in again.");

    public static readonly Error TokenRevoked =
        Error.Unauthorized("REFRESH_TOKEN_REVOKED", "Refresh token has been revoked. Please log in again.");

    public static readonly Error TokenReuseDetected =
        Error.Unauthorized("REFRESH_TOKEN_REUSE_DETECTED", "Security issue detected. Please log in again.");

    public static readonly Error UserNotFound =
        Error.Unauthorized("REFRESH_USER_NOT_FOUND", "Invalid refresh token.");
}
