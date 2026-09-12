using exam_system.Features.Identity.RefreshToken.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.RefreshToken.Handlers;

public class MarkRefreshTokenUsedCommandHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo,
    IUnitOfWork  unitOfWork
    ):IRequestHandler<MarkRefreshTokenUsedCommand,RequestResponse<bool>>
{
    public async Task<RequestResponse<bool>> Handle(MarkRefreshTokenUsedCommand request, CancellationToken cancellationToken)
    {
        var token = await refreshTokenRepo.GetByIdAsync(request.TokenId);

        if (token is null)
        {
            return RequestResponse<bool>.Ok(false);
        }

        token.IsUsed = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return RequestResponse<bool>.Ok(true);
    }
}