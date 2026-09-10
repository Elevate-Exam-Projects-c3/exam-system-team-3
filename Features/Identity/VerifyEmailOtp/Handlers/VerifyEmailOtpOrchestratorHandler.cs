using System.Security.Cryptography;
using System.Text;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Request;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Features.Identity.VerifyEmailOtp.Validators;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;

public class VerifyEmailOtpOrchestrator(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    VerifyEmailOtpRequestValidator validator,
    ILogger<VerifyEmailOtpOrchestrator> logger)
{
    public async Task<RequestResponse<VerifyEmailOtpResponse>> VerifyAsync(
        VerifyEmailOtpRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(f => f.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

            return RequestResponse<VerifyEmailOtpResponse>.Fail(
                "Validation failed.", StatusCodes.Status400BadRequest, errors);
        }

        var normalizedEmail = request.Email.Trim().ToLower();

        var user = await mediator.Send(
            new GetUserByEmailQuery(normalizedEmail), cancellationToken);

        if (user is null)
            return RequestResponse<VerifyEmailOtpResponse>.Fail(VerifyEmailOtpErrors.UserNotFound);

        var otp = await mediator.Send(
            new GetActiveOtpQuery(normalizedEmail), cancellationToken);

        if (otp is null)
            return RequestResponse<VerifyEmailOtpResponse>.Fail(VerifyEmailOtpErrors.OtpNotFound);

        if (otp.AttemptCount >= 5)
            return RequestResponse<VerifyEmailOtpResponse>.Fail(VerifyEmailOtpErrors.OtpLocked);

        if (otp.ExpiresAt < DateTime.UtcNow)
            return RequestResponse<VerifyEmailOtpResponse>.Fail(VerifyEmailOtpErrors.OtpExpired);

        var inputHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.Otp)));

        var isValid = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(inputHash),
            Encoding.UTF8.GetBytes(otp.OtpHash));

        if (!isValid)
        {
            await mediator.Send(new MarkOtpAttemptFailedCommand(otp.Id), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogWarning(
                "Failed OTP verification attempt for {Email}", normalizedEmail);

            return RequestResponse<VerifyEmailOtpResponse>.Fail(VerifyEmailOtpErrors.OtpInvalid);
        }

        await mediator.Send(new ActivateUserCommand(user.Id), cancellationToken);
        await mediator.Send(new MarkOtpUsedCommand(otp.Id), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return RequestResponse<VerifyEmailOtpResponse>.Ok(
            new VerifyEmailOtpResponse(
                user.Id,
                user.Email,
                "Email verified successfully. You can now log in."));
    }
}