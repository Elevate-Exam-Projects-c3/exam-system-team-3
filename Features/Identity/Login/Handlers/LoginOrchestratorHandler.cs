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
    : IRequestHandler<LoginOrchestrator, RequestResponse<LoginResponse>>
{
    public async Task<RequestResponse<LoginResponse>> Handle(
        LoginOrchestrator request,
        CancellationToken cancellationToken)
    {
       
        var authResult = await mediator.Send(
            new AuthenticateUserCommand(request.Email, request.Password),
            cancellationToken);

        if (!authResult.Success)
        {
            return RequestResponse<LoginResponse>.Fail(
                authResult.Message, authResult.StatusCode, authResult.Errors);
        }

        var authenticatedUser = authResult.Data!;

        var accessToken = tokenService.GenerateAccessToken(authenticatedUser.UserId, authenticatedUser.Email, authenticatedUser.Role);

        var refreshTokenResult = await mediator.Send(
            new CreateRefreshTokenCommand(authenticatedUser.UserId),
            cancellationToken);

        if (!refreshTokenResult.Success)
        {
            return RequestResponse<LoginResponse>.Fail(
                refreshTokenResult.Message, refreshTokenResult.StatusCode, refreshTokenResult.Errors);
        }

        refreshTokenCarrier.RawRefreshToken = refreshTokenResult.Data;

        return RequestResponse<LoginResponse>.Ok(
            new LoginResponse(accessToken, authenticatedUser.Role.ToString(), authenticatedUser.UserId));
    }
}