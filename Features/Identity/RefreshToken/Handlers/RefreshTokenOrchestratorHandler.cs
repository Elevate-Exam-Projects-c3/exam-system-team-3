using exam_system.Common.Auth.Jwt;
using exam_system.Common.Auth.RefreshToken;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Identity.RefreshToken.Commands;
using exam_system.Features.Identity.RefreshToken.DTOs.Response;
using exam_system.Features.Identity.RefreshToken.Orchestrators;
using exam_system.Features.Identity.RefreshToken.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class RefreshTokenOrchestratorHandler
(
    IMediator mediator,
    ITokenService tokenService,
    IRefreshTokenCarrier refreshTokenCarrier)
:IRequestHandler<RefreshTokenOrchestrator,RequestResponse<RefreshTokenResponse>>
{
    public async Task<RequestResponse<RefreshTokenResponse>> Handle(RefreshTokenOrchestrator request, CancellationToken cancellationToken)
    {
        var lookUpResult = await mediator.Send(
            new GetRefreshTokenByHashQuery(request.RawRefreshToken), cancellationToken);


        if (!lookUpResult.Success)
        {
            refreshTokenCarrier.ShouldClearCookie = true;
            return RequestResponse<RefreshTokenResponse>.Fail(
                RefreshErrors.TokenNotFound);
        }

        var tokenInfo = lookUpResult.Data!;

        if (tokenInfo.IsRevoked)
        {
            refreshTokenCarrier.ShouldClearCookie = true;
            return RequestResponse<RefreshTokenResponse>.Fail(
                RefreshErrors.TokenRevoked);
        }

        if (tokenInfo.IsUsed)
        {
            await mediator.Send(
                new RevokeAllUserRefreshTokensCommand(tokenInfo.UserId),cancellationToken);
            return RequestResponse<RefreshTokenResponse>.Fail(
                RefreshErrors.TokenReuseDetected);
        }

        if (tokenInfo.ExpiresAt < DateTime.UtcNow)
        {
            refreshTokenCarrier.ShouldClearCookie = true;
            
            return RequestResponse<RefreshTokenResponse>.Fail(
                RefreshErrors.TokenExpired);
        }


        await mediator.Send(new MarkRefreshTokenUsedCommand(tokenInfo.Id), cancellationToken);


        var userResult = await mediator.Send(new GetUserByIdQuery(tokenInfo.UserId), cancellationToken);

        if (!userResult.Success)
        {
            refreshTokenCarrier.ShouldClearCookie = true;
            
            return RequestResponse<RefreshTokenResponse>.Fail(
                RefreshErrors.UserNotFound);
        }

        var refreshedUser = userResult.Data!;

        var accessToken = tokenService.GenerateAccessToken(
            refreshedUser.UserId, refreshedUser.Email, refreshedUser.Role);

        var newRefreshTokenResult = await mediator.Send(
            new CreateRefreshTokenCommand(refreshedUser.UserId), cancellationToken);
        if (!newRefreshTokenResult.Success)
        {
            return RequestResponse<RefreshTokenResponse>.Fail(
                newRefreshTokenResult.Message,
                newRefreshTokenResult.StatusCode,
                newRefreshTokenResult.Errors);
        }

        refreshTokenCarrier.RawRefreshToken = newRefreshTokenResult.Data;
        
        
        
        return RequestResponse<RefreshTokenResponse>.Ok(
            new RefreshTokenResponse(accessToken,refreshedUser.Role.ToString(),refreshedUser.UserId));

    }
}