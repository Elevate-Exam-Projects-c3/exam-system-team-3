using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using exam_system.Features.Identity.ForgotPassword.Queries;
using exam_system.Features.Identity.RefreshToken.Commands;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class ResetPasswordOrchestratorHandler(
    IMediator mediator)
    : IRequestHandler<ResetPasswordOrchestrator, Result<ResetPasswordResponse>>
{
    public async Task<Result<ResetPasswordResponse>> Handle(
        ResetPasswordOrchestrator request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var tokenInfo = await mediator.Send(
            new GetPasswordResetOtpByTokenQuery(normalizedEmail, request.ResetToken), cancellationToken);

        if (tokenInfo is null || tokenInfo.ResetTokenExpiresAt is null
            || tokenInfo.ResetTokenExpiresAt < DateTime.UtcNow)
        {
            return ResetPasswordErrors.InvalidResetToken;
        }

        var updateResult = await mediator.Send(
            new UpdateUserPasswordCommand(tokenInfo.UserId, request.NewPassword), cancellationToken);

        if (!updateResult.IsSuccess)
        {
            return Result<ResetPasswordResponse>.Failure(updateResult.Errors);
        }

        await mediator.Send(new InvalidateResetTokenCommand(tokenInfo.Id), cancellationToken);

 
        await mediator.Send(new RevokeAllUserRefreshTokensCommand(tokenInfo.UserId), cancellationToken);

        return Result<ResetPasswordResponse>.Success(
            new ResetPasswordResponse("Password has been reset successfully. Please log in with your new password."));
    }
}