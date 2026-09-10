using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Internal;

namespace exam_system.Features.Identity.VerifyEmailOtp.Queries;

public record GetActiveOtpQuery(string Email) : IRequest<OtpLookupResult?>;