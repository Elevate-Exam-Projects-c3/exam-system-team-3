using exam_system.Features.Identity.ForgotPassword.DTOs.Internal;

namespace exam_system.Features.Identity.ForgotPassword.Queries;

public record GetLatestPasswordResetOtpQuery(string Email) : IRequest<PasswordResetOtpCooldownResult?>;