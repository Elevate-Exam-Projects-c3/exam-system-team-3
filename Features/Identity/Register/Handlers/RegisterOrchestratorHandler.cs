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
    : IRequestHandler<RegisterOrchestrator, Result<RegisterUserResponse>>
{
    public async Task<Result<RegisterUserResponse>> Handle(
        RegisterOrchestrator request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLower();

        var emailExists = await mediator.Send(
            new CheckEmailExistsQuery(normalizedEmail), cancellationToken);

        if (emailExists)
        {
            return Result<RegisterUserResponse>.Failure(
                RegisterUserErrors.EmailAlreadyRegistered);
        }

        var createUserResult = await mediator.Send(
            new CreateUserCommand(request.FullName, normalizedEmail, request.Password),
            cancellationToken);

        //if (!createUserResult.Success)
        //{
        //    return Result<RegisterUserResponse>.Failure(
        //        createUserResult.Message,
        //        createUserResult.StatusCode,
        //        createUserResult.Errors);
        //}

        //var userId = createUserResult.Data!;
        if (createUserResult.IsError)
        {
            return Result<RegisterUserResponse>.Failure(createUserResult.Errors);
        }

        var userId = createUserResult.Value;

        var createOtpResult = await mediator.Send(
            new CreateEmailOtpCommand(userId, normalizedEmail),
            cancellationToken);

        if (createOtpResult.IsError)
        {
            return Result<RegisterUserResponse>.Failure(
                createOtpResult.Errors);
        }

        var plainOtp = createOtpResult.Value!;

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

        return Result<RegisterUserResponse>.Success(
            new RegisterUserResponse(userId, normalizedEmail, "Registration successful. Please verify your email."));
    }
}