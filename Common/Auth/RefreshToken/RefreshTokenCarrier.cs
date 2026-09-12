namespace exam_system.Common.Auth.RefreshToken;

public class RefreshTokenCarrier : IRefreshTokenCarrier
{
    public string? RawRefreshToken { get; set; }
    public bool ShouldClearCookie { get; set; }
}