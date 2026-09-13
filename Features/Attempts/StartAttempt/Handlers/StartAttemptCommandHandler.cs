using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.StartAttempt.Commands;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class StartAttemptCommandHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<StartAttemptCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(StartAttemptCommand request,CancellationToken cancellationToken)
        {
            var startTime = DateTime.UtcNow;

            var attempt = new QuizAttempt
            {
                StudentId = request.StudentId,

                QuizId = request.QuizId,

                Status = AttemptStatus.InProgress,

                StartTime = startTime,

                Deadline = startTime.AddMinutes(request.DurationMinutes)
            };

            await _attemptRepository.AddAsync(attempt);

            await _attemptRepository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(attempt.Id);
        }
    }
}
