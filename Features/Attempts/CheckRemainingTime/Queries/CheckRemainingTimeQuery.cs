using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.DTOs;

namespace exam_system.Features.Attempts.CheckRemainingTime.Queries
{
    public record CheckRemainingTimeQuery(Guid AttemptId, Guid StudentId) : IRequest<Result<RemainingTimeResponse>>;
}
