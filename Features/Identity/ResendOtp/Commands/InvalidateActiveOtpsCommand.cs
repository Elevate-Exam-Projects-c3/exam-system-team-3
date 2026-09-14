using Org.BouncyCastle.Ocsp;

namespace exam_system.Features.Identity.ResendOtp.Commands;

public record InvalidateActiveOtpsCommand(
    string Email):IRequest;
    