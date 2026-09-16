using System.Security.Cryptography;
using System.Text;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs.Response;
using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers;

public class VerifyEmailOtpOrchestratorHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    ILogger<VerifyEmailOtpOrchestratorHandler> logger)
    : IRequestHandler<VerifyEmailOtpOrchestrator, Result<VerifyEmailOtpResponse>>
{
    public async Task<Result<VerifyEmailOtpResponse>> Handle(
        VerifyEmailOtpOrchestrator request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var user = await mediator.Send(
            new GetUserByEmailQuery(normalizedEmail), cancellationToken);

        if (user is null)
            return Result<VerifyEmailOtpResponse>.Failure(VerifyEmailOtpErrors.UserNotFound);

        var otp = await mediator.Send(
            new GetActiveOtpQuery(normalizedEmail), cancellationToken);

        if (otp is null)
            return Result<VerifyEmailOtpResponse>.Failure(VerifyEmailOtpErrors.OtpNotFound);

        if (otp.AttemptCount >= 5)
            return Result<VerifyEmailOtpResponse>.Failure(VerifyEmailOtpErrors.OtpLocked);

        if (otp.ExpiresAt < DateTime.UtcNow)
            return Result<VerifyEmailOtpResponse>.Failure(VerifyEmailOtpErrors.OtpExpired);

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

            return Result<VerifyEmailOtpResponse>.Failure(VerifyEmailOtpErrors.OtpInvalid);
        }

        await mediator.Send(new ActivateUserCommand(user.Id), cancellationToken);
        await mediator.Send(new MarkOtpUsedCommand(otp.Id), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<VerifyEmailOtpResponse>.Success(
            new VerifyEmailOtpResponse(
                user.Id,
                user.Email,
                "Email verified successfully. You can now log in."));
    }
}