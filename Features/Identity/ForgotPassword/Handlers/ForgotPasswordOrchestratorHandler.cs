using exam_system.Common.Email;
using exam_system.Features.Identity.ForgotPassword.DTOs.Response;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using exam_system.Features.Identity.ForgotPassword.Queries;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;


namespace exam_system.Features.Identity.ForgotPassword.Handlers;

public class ForgotPasswordOrchestratorHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IEmailService  emailService,
    ILogger<ForgotPasswordOrchestratorHandler> logger): IRequestHandler<ForgotPasswordOrchestrator,RequestResponse<ForgotPasswordResponse>>
{
    private const string NeutralMessage = "If this email is registered, a code has been sent.";

    public async Task <RequestResponse<ForgotPasswordResponse>> Handle(
        ForgotPasswordOrchestrator request, 
        CancellationToken cancellationToken)
    {
        
        var normalizedEmail = request.Email.Trim().ToLower();


        var latestOtp = await mediator.Send(
            new GetLatestPasswordResetOtpQuery(normalizedEmail), cancellationToken);
        if (latestOtp is not null && latestOtp.CreatedAt.AddSeconds(30) > DateTime.UtcNow)
        {
            return RequestResponse<ForgotPasswordResponse>.Fail(
                ForgotPasswordErrors.ResendTooSoon);
        }

        var user = await mediator.Send(
            new GetUserByEmailQuery(normalizedEmail), cancellationToken);

        if (user is not null)
        {
            var otpResult = await mediator.Send(
                new CreatePasswordResetOtpCommand(user.Id, normalizedEmail), cancellationToken);
            if (otpResult.Success)
            {
                
            await unitOfWork.SaveChangesAsync(cancellationToken);
            try
            {
                await emailService.SendOtpEmailAsync(normalizedEmail, otpResult.Data!, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Failed to send password reset OTP email to {Email}.",
                    normalizedEmail);
            }
            }

        }

        return RequestResponse<ForgotPasswordResponse>.Ok(
            new ForgotPasswordResponse(NeutralMessage));
    }
}