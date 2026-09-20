using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitAttempt.Commands;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public class SubmitQuizAttemptCommandHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<SubmitQuizAttemptCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(SubmitQuizAttemptCommand request,CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.GetByIdAsync(
                request.AttemptId,
                x => x.Answers);

            if (attempt is null || attempt.IsDeleted)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "ATTEMPT_NOT_FOUND",
                        "Quiz attempt was not found."));
            }

            if (attempt.Status != AttemptStatus.InProgress)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "ATTEMPT_ALREADY_FINISHED",
                        "Quiz attempt has already been submitted or timed out."));
            }

            foreach (var answer in attempt.Answers)
            {
                if (request.AnswerCorrectness.TryGetValue(
                        answer.Id,
                        out var isCorrect))
                {
                    answer.IsCorrect = isCorrect;
                }
            }

            attempt.Score = request.Score;
            attempt.Passed = request.Passed;
            attempt.SubmittedAt = request.SubmittedAt;
            attempt.Status = request.Status;

            await _attemptRepository.SaveChangesAsync(
                cancellationToken);

            return Result<Guid>.Success(attempt.Id);
        }
    }
}
