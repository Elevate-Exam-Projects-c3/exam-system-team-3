using System.Security.Cryptography;
using System.Text;
using exam_system.Features.Identity.Logout.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Logout.Handlers;

public class RevokeRefreshTokenCommandHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RevokeRefreshTokenCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        RevokeRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var tokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.RawRefreshToken)));

        var token = await refreshTokenRepo
            .Get(t => t.Token == tokenHash)
            .FirstOrDefaultAsync(cancellationToken);

        if (token is null)
        {
            return Result<bool>.Success(true);
        }

        token.IsRevoked = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}