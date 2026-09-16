using System.Security.Cryptography;
using System.Text;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using exam_system.Features.Identity.ForgotPassword.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class VerifyResetOtpOrchestratorHandler(
    IMediator mediator,
    ILogger<VerifyResetOtpOrchestratorHandler> logger)
    : IRequestHandler<VerifyResetOtpOrchestrator, Result<VerifyResetOtpResponse>>
{
    public async Task<Result<VerifyResetOtpResponse>> Handle(
        VerifyResetOtpOrchestrator request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var otp = await mediator.Send(
            new GetActivePasswordResetOtpQuery(normalizedEmail), cancellationToken);

        if (otp is null)
        {
            return VerifyResetOtpErrors.OtpNotFound;
        }

        if (otp.AttemptCount >= 5)
        {
            return VerifyResetOtpErrors.OtpLocked;
        }

        if (otp.ExpiresAt < DateTime.UtcNow)
        {
            return VerifyResetOtpErrors.OtpExpired;
        }

        var inputHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.Otp)));

        var isValid = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(inputHash),
            Encoding.UTF8.GetBytes(otp.OtpHash));

        if (!isValid)
        {
            await mediator.Send(new MarkPasswordResetOtpAttemptFailedCommand(otp.Id), cancellationToken);

            logger.LogWarning("Failed password reset OTP verification attempt for {Email}", normalizedEmail);

            return VerifyResetOtpErrors.OtpInvalid;
        }

        var tokenResult = await mediator.Send(new IssueResetTokenCommand(otp.Id), cancellationToken);

        if (!tokenResult.IsSuccess)
        {
            return Result<VerifyResetOtpResponse>.Failure(tokenResult.Errors);
        }

        return Result<VerifyResetOtpResponse>.Success(new VerifyResetOtpResponse(tokenResult.Value));
    }
}