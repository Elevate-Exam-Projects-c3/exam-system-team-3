using exam_system.Common.Email;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Identity.ResendOtp.Commands;
using exam_system.Features.Identity.ResendOtp.DTOs.Response;
using exam_system.Features.Identity.ResendOtp.Orchestrators;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.ResendOtp.Handlers;

public class ResendOtpOrchestratorHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IEmailService  emailService,
    ILogger<ResendOtpOrchestratorHandler> logger
    ):IRequestHandler<ResendOtpOrchestrator,Result<ResendOtpResponse>>
{
    public async Task<Result<ResendOtpResponse>> Handle(ResendOtpOrchestrator request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();


        var user = await mediator.Send(
            new GetUserByEmailQuery(normalizedEmail), cancellationToken);
        if (user is null)
        {
            return Result<ResendOtpResponse>.Failure(ResendOtpErrors.UserNotFound);
        }

        if (user.EmailConfirmed)
        {
            return Result<ResendOtpResponse>.Failure(ResendOtpErrors.AlreadyVerified);
        }

        await mediator.Send(new InvalidateActiveOtpsCommand(normalizedEmail), cancellationToken);

        var createOtpResult = await mediator.Send(
            new CreateEmailOtpCommand(user.Id, normalizedEmail), cancellationToken);

        if (createOtpResult.IsError)
        {
            return Result<ResendOtpResponse>.Failure(createOtpResult.Errors);
        }


        var plainOtp = createOtpResult.Value!;

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

        return Result<ResendOtpResponse>.Success(
            new ResendOtpResponse(normalizedEmail, "A new verification code has been sent to your email."));
    }
}