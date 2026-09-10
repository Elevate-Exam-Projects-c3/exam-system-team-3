using exam_system.Common.Email;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Identity.Register.DTOs.Response;
using exam_system.Features.Identity.Register.Orchestrators;
using exam_system.Features.Identity.Register.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Register.Handlers;

public class RegisterOrchestratorHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    ILogger<RegisterOrchestratorHandler> logger)
    : IRequestHandler<RegisterOrchestratorRequest, RequestResponse<RegisterUserResponse>>
{
    public async Task<RequestResponse<RegisterUserResponse>> Handle(
        RegisterOrchestratorRequest request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var emailExists = await mediator.Send(
            new CheckEmailExistsQuery(normalizedEmail), cancellationToken);

        if (emailExists)
        {
            return RequestResponse<RegisterUserResponse>.Fail(
                RegisterUserErrors.EmailAlreadyRegistered);
        }

        var createUserResult = await mediator.Send(
            new CreateUserCommand(request.FullName, normalizedEmail, request.Password),
            cancellationToken);

        if (!createUserResult.Success)
        {
            return RequestResponse<RegisterUserResponse>.Fail(
                createUserResult.Message,
                createUserResult.StatusCode,
                createUserResult.Errors);
        }

        var userId = createUserResult.Data!;

        var createOtpResult = await mediator.Send(
            new CreateEmailOtpCommand(userId, normalizedEmail),
            cancellationToken);

        if (!createOtpResult.Success)
        {
            return RequestResponse<RegisterUserResponse>.Fail(
                createOtpResult.Message,
                createOtpResult.StatusCode,
                createOtpResult.Errors);
        }

        var plainOtp = createOtpResult.Data!;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            await emailService.SendOtpEmailAsync(normalizedEmail, plainOtp, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to send OTP email to {Email} for newly registered user {UserId}. User account created successfully but requires manual OTP resend.",
                normalizedEmail,
                userId);
        }

        return RequestResponse<RegisterUserResponse>.Created(
            new RegisterUserResponse(userId, normalizedEmail, "Registration successful. Please verify your email."));
    }
}