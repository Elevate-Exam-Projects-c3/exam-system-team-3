using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.RefreshToken.DTOs.Internal;
using exam_system.Features.Identity.RefreshToken.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results.ErrorCodes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.RefreshToken.Handlers;

public  class GetUserByIdQueryHandler(
    IGenericRepository<ApplicationUser> userRepo)
:IRequestHandler<GetUserByIdQuery,RequestResponse<RefreshedUserResult>>
{
    public async Task<RequestResponse<RefreshedUserResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return RequestResponse<RefreshedUserResult>.Fail(
                RefreshErrors.UserNotFound);
        }
        
        return RequestResponse<RefreshedUserResult>.Ok(
            new RefreshedUserResult (user.Id,user.Email,user.Role));
        
    }
}