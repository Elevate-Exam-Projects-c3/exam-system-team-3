using System.Security.Cryptography;
using System.Text;
using exam_system.Common.Auth.Jwt;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Login.Handlers;

public class CreateRefreshTokenCommandHandler(
    IGenericRepository<Domain.Entities.Identity.RefreshToken> refreshTokenRepo,
    IUnitOfWork unitOfWork,
    ITokenService tokenService)
    : IRequestHandler<CreateRefreshTokenCommand, RequestResponse<string>>
{
    public async Task<RequestResponse<string>> Handle(
        CreateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var rawRefreshToken = tokenService.GenerateRefreshToken();
        var refreshTokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(rawRefreshToken)));

        var refreshTokenEntity = new Domain.Entities.Identity.RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Token = refreshTokenHash,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsUsed = false,
            IsRevoked = false
        };

        await refreshTokenRepo.AddAsync(refreshTokenEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return RequestResponse<string>.Ok(rawRefreshToken);
    }
}