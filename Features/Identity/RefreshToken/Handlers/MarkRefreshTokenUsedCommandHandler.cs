using exam_system.Features.Identity.RefreshToken.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class MarkRefreshTokenUsedCommandHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo,
    IUnitOfWork  unitOfWork
    ):IRequestHandler<MarkRefreshTokenUsedCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(MarkRefreshTokenUsedCommand request, CancellationToken cancellationToken)
    {
        var token = await refreshTokenRepo.GetByIdAsync(request.TokenId);

        if (token is null)
        {
            return Result<bool>.Success(false);
        }

        token.IsUsed = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}