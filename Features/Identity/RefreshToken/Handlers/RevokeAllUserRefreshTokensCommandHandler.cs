using exam_system.Features.Identity.RefreshToken.Commands;


namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class RevokeAllUserRefreshTokensCommandHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo,
    IUnitOfWork unitOfWork)
:IRequestHandler<RevokeAllUserRefreshTokensCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(RevokeAllUserRefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var tokens = await refreshTokenRepo
            .Get(t => t.UserId == request.UserId && !t.IsRevoked)
            .ToListAsync(cancellationToken);
        
        foreach(var token in tokens)
        {
            token.IsRevoked = true;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Updated>.Success(Result.Updated);

    }
}