using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Internal;

namespace exam_system.Features.Identity.VerifyEmailOtp.Queries;

public record GetUserByEmailQuery(string Email) : IRequest<UserLookupResult?>;