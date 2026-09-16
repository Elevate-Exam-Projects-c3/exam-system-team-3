using exam_system.Common.Auth.Jwt;
using exam_system.Common.Auth.RefreshToken;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Identity.Login.DTOs.Response;
using exam_system.Features.Identity.Login.Orchestrators;
using exam_system.Features.Shared;

namespace exam_system.Features.Identity.Login.Handlers;

public class LoginOrchestratorHandler(
    IMediator mediator,
    ITokenService tokenService,
    IRefreshTokenCarrier refreshTokenCarrier)
    : IRequestHandler<LoginOrchestrator, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(
        LoginOrchestrator request,
        CancellationToken cancellationToken)
    {
       
        var authResult = await mediator.Send(
            new AuthenticateUserCommand(request.Email, request.Password),
            cancellationToken);

        if (authResult.IsError)
        {
            return Result<LoginResponse>.Failure(authResult.Errors);
        }

        var authenticatedUser = authResult.Value;

        var accessToken = tokenService.GenerateAccessToken(authenticatedUser.UserId, authenticatedUser.Email, authenticatedUser.Role);

        var refreshTokenResult = await mediator.Send(
            new CreateRefreshTokenCommand(authenticatedUser.UserId),
            cancellationToken);

        //if (!refreshTokenResult.Success)
        //{
        //    return Result<LoginResponse>.Failure(
        //        refreshTokenResult.Message, refreshTokenResult.StatusCode, refreshTokenResult.Errors);
        //}
        if (refreshTokenResult.IsError)
        {
            return Result<LoginResponse>.Failure(refreshTokenResult.Errors);
        }

        refreshTokenCarrier.RawRefreshToken = refreshTokenResult.Value;

        return Result<LoginResponse>.Success(
            new LoginResponse(accessToken, authenticatedUser.Role.ToString(), authenticatedUser.UserId));
    }
}