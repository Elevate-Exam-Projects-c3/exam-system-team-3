using exam_system.Features.Identity.RefreshToken.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class MarkRefreshTokenUsedCommandHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo,
    IUnitOfWork  unitOfWork
    ):IRequestHandler<MarkRefreshTokenUsedCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(MarkRefreshTokenUsedCommand request, CancellationToken cancellationToken)
    {
        var token = await refreshTokenRepo.GetByIdAsync(request.TokenId);

        if (token is null)
        {
          return RefreshErrors.TokenNotFound;
        }

        token.IsUsed = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Updated;
    }
}