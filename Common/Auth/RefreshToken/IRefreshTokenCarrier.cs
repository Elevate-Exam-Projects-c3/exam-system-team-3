namespace exam_system.Common.Auth.RefreshToken;

public interface IRefreshTokenCarrier
{
    string? RawRefreshToken { get; set; }
}