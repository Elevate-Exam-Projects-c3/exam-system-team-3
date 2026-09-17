using exam_system.Features.Identity.ForgotPassword.DTOs.Internal;

namespace exam_system.Features.Identity.ForgotPassword.Queries;

public record GetPasswordResetOtpByTokenQuery(string Email, string ResetToken) : IRequest<ResetTokenLookupResult?>;