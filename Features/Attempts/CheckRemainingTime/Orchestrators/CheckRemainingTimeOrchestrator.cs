using exam_system.Features.Attempts.CheckRemainingTime.DTOs;

namespace exam_system.Features.Attempts.CheckRemainingTime.Orchestrators
{
    public record CheckRemainingTimeOrchestrator(Guid AttemptId,Guid StudentId) : IRequest<Result<RemainingTimeResponse>>;
}
