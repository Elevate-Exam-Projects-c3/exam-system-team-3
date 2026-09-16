using exam_system.Common.Auth.Jwt;
using exam_system.Common.Auth.RefreshToken;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Identity.RefreshToken.Commands;
using exam_system.Features.Identity.RefreshToken.DTOs.Response;
using exam_system.Features.Identity.RefreshToken.Orchestrators;
using exam_system.Features.Identity.RefreshToken.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class RefreshTokenOrchestratorHandler
(
    IMediator mediator,
    ITokenService tokenService,
    IRefreshTokenCarrier refreshTokenCarrier)
:IRequestHandler<RefreshTokenOrchestrator, Result<RefreshTokenResponse>>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenOrchestrator request, CancellationToken cancellationToken)
    {
        var lookUpResult = await mediator.Send(
            new GetRefreshTokenByHashQuery(request.RawRefreshToken), cancellationToken);


        //if (!lookUpResult.Success)
        //{
        //    refreshTokenCarrier.ShouldClearCookie = true;
        //    return Result<RefreshTokenResponse>.Failure(
        //        RefreshErrors.TokenNotFound);
        //}
        
        if (lookUpResult.IsError)
        {
            refreshTokenCarrier.ShouldClearCookie = true;

            return Result<RefreshTokenResponse>.Failure(lookUpResult.Errors);
        }

        //var tokenInfo = lookUpResult.Data!;
        var tokenInfo = lookUpResult.Value;

        if (tokenInfo.IsRevoked)
        {
            refreshTokenCarrier.ShouldClearCookie = true;
            return Result<RefreshTokenResponse>.Failure(
                RefreshErrors.TokenRevoked);
        }

        if (tokenInfo.IsUsed)
        {
            await mediator.Send(
                new RevokeAllUserRefreshTokensCommand(tokenInfo.UserId),cancellationToken);
            return Result<RefreshTokenResponse>.Failure(
                RefreshErrors.TokenReuseDetected);
        }

        if (tokenInfo.ExpiresAt < DateTime.UtcNow)
        {
            refreshTokenCarrier.ShouldClearCookie = true;
            
            return Result<RefreshTokenResponse>.Failure(
                RefreshErrors.TokenExpired);
        }


        await mediator.Send(new MarkRefreshTokenUsedCommand(tokenInfo.Id), cancellationToken);


        var userResult = await mediator.Send(new GetUserByIdQuery(tokenInfo.UserId), cancellationToken);

        //if (!userResult.Success)
        //{
        //    refreshTokenCarrier.ShouldClearCookie = true;
            
        //    return Result<RefreshTokenResponse>.Failure(
        //        RefreshErrors.UserNotFound);
        //}

        //var refreshedUser = userResult.Data!;
        if (userResult.IsError)
        {
            refreshTokenCarrier.ShouldClearCookie = true;

            return Result<RefreshTokenResponse>.Failure(userResult.Errors);
        }

        var refreshedUser = userResult.Value;

        var accessToken = tokenService.GenerateAccessToken(
            refreshedUser.UserId, refreshedUser.Email, refreshedUser.Role);

        var newRefreshTokenResult = await mediator.Send(
            new CreateRefreshTokenCommand(refreshedUser.UserId), cancellationToken);
        //if (!newRefreshTokenResult.Success)
        //{
        //    return Result<RefreshTokenResponse>.Failure(
        //        newRefreshTokenResult.Message,
        //        newRefreshTokenResult.StatusCode,
        //        newRefreshTokenResult.Errors);
        //}
        if (newRefreshTokenResult.IsError)
        {
            return Result<RefreshTokenResponse>.Failure(
                newRefreshTokenResult.Errors);
        }
        refreshTokenCarrier.RawRefreshToken = newRefreshTokenResult.Value;
        return Result<RefreshTokenResponse>.Success(
            new RefreshTokenResponse(accessToken,refreshedUser.Role.ToString(),refreshedUser.UserId));

    }
}
