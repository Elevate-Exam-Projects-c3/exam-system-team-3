using exam_system.Common.Email;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Identity.ResendOtp.Commands;
using exam_system.Features.Identity.ResendOtp.DTOs.Response;
using exam_system.Features.Identity.ResendOtp.Orchestrators;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace exam_system.Features.Identity.ResendOtp.Handlers;

public class ResendOtpOrchestratorHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IEmailService  emailService,
    ILogger<ResendOtpOrchestratorHandler> logger
    ):IRequestHandler<ResendOtpOrchestrator,RequestResponse<ResendOtpResponse>>
{
    public async Task<RequestResponse<ResendOtpResponse>> Handle(ResendOtpOrchestrator request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();


        var user = await mediator.Send(
            new GetUserByEmailQuery(normalizedEmail), cancellationToken);
        if (user is null)
        {
            return RequestResponse<ResendOtpResponse>.Fail(
                ResendOtpErrors.UserNotFound);
        }

        if (user.EmailConfirmed)
        {
            return RequestResponse<ResendOtpResponse>.Fail(
                ResendOtpErrors.AlreadyVerified);
        }

        await mediator.Send(new InvalidateActiveOtpsCommand(normalizedEmail), cancellationToken);

        var createOtpResult = await mediator.Send(
            new CreateEmailOtpCommand(user.Id, normalizedEmail), cancellationToken);

        if (!createOtpResult.Success)
        {
            return RequestResponse<ResendOtpResponse>.Fail(
                createOtpResult.Message, createOtpResult.StatusCode, createOtpResult.Errors);
        }


        var plainOtp = createOtpResult.Data!;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            await emailService.SendOtpEmailAsync(normalizedEmail, plainOtp, cancellationToken);
        }
        catch(Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to resend OTP email to {Email}. New OTP was generated and stored, but delivery failed.",
                normalizedEmail);
            
        }

        return RequestResponse<ResendOtpResponse>.Ok(
            new ResendOtpResponse(normalizedEmail, "A new verification code has been sent to your email."));
    }
}