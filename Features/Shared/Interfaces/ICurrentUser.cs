namespace exam_system.Features.Shared.Interfaces;

public interface ICurrentUser
{
    Guid? UserId { get; }
}
