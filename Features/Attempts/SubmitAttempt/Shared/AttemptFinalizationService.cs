using exam_system.Features.Attempts.SubmitAttempt.Commands;
using exam_system.Features.Attempts.SubmitAttempt.Queries;
using exam_system.Features.Attempts.SubmitAttempt.ViewModels;

namespace exam_system.Features.Attempts.SubmitAttempt.Shared
{
    public class AttemptFinalizationService(IMediator _mediator) : IAttemptFinalizationService
    {
        public async Task<Result<SubmitQuizAttemptResponse>> SubmitAsync(Guid attemptId,CancellationToken cancellationToken)
        {
            var attempt = await _mediator.Send(
                new GetAttemptForFinalizationQuery(attemptId),
                cancellationToken);

            if (attempt is null)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.NotFound(
                        "ATTEMPT_NOT_FOUND",
                        "Quiz attempt was not found."));
            }

            if (attempt.Status != AttemptStatus.InProgress)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_ALREADY_FINISHED",
                        "Quiz attempt has already been submitted or timed out."));
            }

            var now = DateTime.UtcNow;

            if (now >= attempt.Deadline)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_EXPIRED",
                        "The quiz attempt deadline has expired."));
            }

            return await FinalizeAsync(
                attempt,
                AttemptStatus.Submitted,
                now,
                cancellationToken);
        }

        public async Task<Result<SubmitQuizAttemptResponse>> TimeoutAsync(
            Guid attemptId,
            CancellationToken cancellationToken)
        {
            var attempt = await _mediator.Send(
                new GetAttemptForFinalizationQuery(attemptId),
                cancellationToken);

            if (attempt is null)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.NotFound(
                        "ATTEMPT_NOT_FOUND",
                        "Quiz attempt was not found."));
            }

            if (attempt.Status != AttemptStatus.InProgress)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_ALREADY_FINISHED",
                        "Quiz attempt has already been submitted or timed out."));
            }

            var now = DateTime.UtcNow;

            if (now < attempt.Deadline)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_NOT_EXPIRED",
                        "The quiz attempt deadline has not expired."));
            }

            return await FinalizeAsync(
                attempt,
                AttemptStatus.TimedOut,
                now,
                cancellationToken);
        }

        private async Task<Result<SubmitQuizAttemptResponse>> FinalizeAsync(
            Domain.Entities.Attempts.QuizAttempt attempt,
            AttemptStatus finalStatus,
            DateTime finalizedAt,
            CancellationToken cancellationToken)
        {
            // Get all answers received before the deadline.
            var answers = await _mediator.Send(
                new GetAttemptAnswersForSubmitQuery(
                    attempt.Id,
                    attempt.Deadline),
                cancellationToken);

            // Get total number of active questions.
            var totalQuestions = await _mediator.Send(
                new GetQuizQuestionCountQuery(
                    attempt.QuizId),
                cancellationToken);

            if (totalQuestions == 0)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.Failure(
                        "QUIZ_HAS_NO_QUESTIONS",
                        "The quiz does not contain any questions."));
            }

            // Calculate correct answers from SelectedOption.IsCorrect.
            var correctAnswers = answers.Count(
                x => x.OptionIsCorrect);

            // Score = (correct answers / total questions) × 100
            var score =
                (double)correctAnswers /
                totalQuestions *
                100;

            // Get quiz pass score.
            var passScore = await _mediator.Send(
                new GetQuizPassScoreQuery(
                    attempt.QuizId),
                cancellationToken);

            if (passScore is null)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    Error.NotFound(
                        "QUIZ_NOT_FOUND",
                        "Quiz was not found."));
            }

            var passed = score >= passScore.Value;

            // Prepare values that will be persisted.
            var answerCorrectness = answers
                .ToDictionary(
                    x => x.AnswerId,
                    x => x.OptionIsCorrect);

            // Persist the final attempt.
            var commandResult = await _mediator.Send(
                new SubmitQuizAttemptCommand(
                    attempt.Id,
                    score,
                    passed,
                    finalizedAt,
                    finalStatus,
                    answerCorrectness),
                cancellationToken);

            if (!commandResult.IsSuccess)
            {
                return Result<SubmitQuizAttemptResponse>.Failure(
                    commandResult.Errors.First());
            }

            return Result<SubmitQuizAttemptResponse>.Success(
                new SubmitQuizAttemptResponse
                {
                    AttemptId = attempt.Id,
                    Score = score,
                    Passed = passed,
                    SubmittedAt = finalizedAt
                });
        }
    }
}
