
using System.Security.Cryptography;
using System.Text;
using exam_system.Features.Identity.RefreshToken.DTOs.Internal;
using exam_system.Features.Identity.RefreshToken.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;


namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class GetRefreshTokenByHashQueryHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo)
:IRequestHandler<GetRefreshTokenByHashQuery,RequestResponse<RefreshTokenLookupResult>>
    
{
    public async Task<RequestResponse<RefreshTokenLookupResult>> Handle(GetRefreshTokenByHashQuery request, CancellationToken cancellationToken)
    {

        var tokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.RawToken)));

        var token = await refreshTokenRepo
            .Get(t => t.Token == tokenHash)
            .FirstOrDefaultAsync(cancellationToken);

        if (token is null)
        {
            return RequestResponse<RefreshTokenLookupResult>.Fail(
                RefreshErrors.TokenNotFound);
        }
        
        return RequestResponse<RefreshTokenLookupResult>.Ok(
            new RefreshTokenLookupResult(token.Id, token.UserId,token.IsUsed,token.IsRevoked,token.ExpiresAt));
        
        

    }
}