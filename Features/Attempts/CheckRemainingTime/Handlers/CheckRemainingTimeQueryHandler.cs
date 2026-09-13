using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.DTOs;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class CheckRemainingTimeQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<CheckRemainingTimeQuery,Result<RemainingTimeResponse>>
    {
        public async Task<Result<RemainingTimeResponse>> Handle(CheckRemainingTimeQuery request,CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository
                .Get(x =>
                    x.Id == request.AttemptId &&
                    x.StudentId == request.StudentId &&
                    x.Status == AttemptStatus.InProgress &&
                    !x.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (attempt is null)
            {
                return Result<RemainingTimeResponse>.Failure(
                    Error.NotFound(
                        "ATTEMPT_NOT_FOUND",
                        "Quiz attempt was not found."));
            }

            var remaining =
                attempt.Deadline - DateTime.UtcNow;

            var remainingSeconds = Math.Max(
                0,
                (int)remaining.TotalSeconds);

            return Result<RemainingTimeResponse>.Success(
                new RemainingTimeResponse
                {
                    AttemptId = attempt.Id,
                    Deadline = attempt.Deadline,
                    RemainingSeconds = remainingSeconds,
                    IsExpired = remainingSeconds <= 0
                });
        }
    }
}
