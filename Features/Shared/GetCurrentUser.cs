using exam_system.Features.Shared.Interfaces;

namespace exam_system.Features.Shared;

public class GetCurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public Guid? UserId
    {
        get
        {
            var userId = httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userId, out var id) ? id : null;
        }
    }
}
