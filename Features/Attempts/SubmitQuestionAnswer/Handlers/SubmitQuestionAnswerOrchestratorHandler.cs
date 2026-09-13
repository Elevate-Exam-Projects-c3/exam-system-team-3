using exam_system.Features.Attempts.SubmitQuestionAnswer.Commands;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;
using exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public sealed class SubmitQuestionAnswerOrchestratorHandler(IMediator _mediator): IRequestHandler<SubmitQuestionAnswerOrchestrator,Result<SubmitQuestionAnswerResponse>>
    {
        public async Task<Result<SubmitQuestionAnswerResponse>> Handle(SubmitQuestionAnswerOrchestrator request,CancellationToken cancellationToken)
        {
            // =========================================================
            // 1. Get attempt and verify ownership
            // =========================================================

            var attempt = await _mediator.Send(new GetAttemptForAnswerQuery(
                    request.AttemptId,
                    request.StudentId),
                cancellationToken);

            if (attempt is null)
            {
                return Result<SubmitQuestionAnswerResponse>.Failure(
                    Error.NotFound(
                        "ATTEMPT_NOT_FOUND",
                        "Quiz attempt was not found."));
            }

            // =========================================================
            // 2. Check attempt status
            // =========================================================

            if (attempt.Status != AttemptStatus.InProgress)
            {
                return Result<SubmitQuestionAnswerResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_NOT_IN_PROGRESS",
                        "This attempt is no longer in progress."));
            }

            // =========================================================
            // 3. Check deadline
            // =========================================================

            var now = DateTime.UtcNow;

            if (now >= attempt.Deadline)
            {
                return Result<SubmitQuestionAnswerResponse>.Failure(
                    Error.Conflict(
                        "ATTEMPT_TIME_EXPIRED",
                        "The time allowed for this attempt has expired."));
            }

            // =========================================================
            // 4. Verify question belongs to this quiz
            // =========================================================

            var question = await _mediator.Send(
                new GetQuestionForAnswerQuery(
                    request.QuestionId,
                    attempt.QuizId),
                cancellationToken);

            if (question is null)
            {
                return Result<SubmitQuestionAnswerResponse>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "The question does not belong to this quiz."));
            }

            // =========================================================
            // 5. Verify selected option belongs to this question
            // =========================================================

            var selectedOption = await _mediator.Send(new GetQuestionOptionForAnswerQuery(
                    request.QuestionId,
                    request.Request.SelectedOptionId!.Value),
                cancellationToken);

            if (selectedOption is null)
            {
                return Result<SubmitQuestionAnswerResponse>.Failure(
                    Error.Validation(
                        "INVALID_OPTION",
                        "The selected option does not belong to this question."));
            }

            // =========================================================
            // 6. Check if answer already exists
            // =========================================================

            var existingAnswer = await _mediator.Send(new GetStudentQuestionAnswerQuery(
                    request.AttemptId,
                    request.QuestionId),
                cancellationToken);

            // =========================================================
            // 7. Decide CREATE or UPDATE
            // =========================================================

            Result<Guid> commandResult;

            if (existingAnswer is null)
            {
                commandResult = await _mediator.Send(new CreateQuestionAnswerCommand(
                        request.AttemptId,
                        request.QuestionId,
                        selectedOption.Id,
                        selectedOption.IsCorrect),
                    cancellationToken);
            }
            else
            {
                commandResult = await _mediator.Send(new UpdateQuestionAnswerCommand(
                        existingAnswer.Id,
                        selectedOption.Id,
                        selectedOption.IsCorrect),
                    cancellationToken);
            }

            // =========================================================
            // 8. Check command result
            // =========================================================

            if (!commandResult.IsSuccess)
            {
                return Result<SubmitQuestionAnswerResponse>.Failure(
                    commandResult.Errors[0]);
            }

            // =========================================================
            // 9. Build response
            // =========================================================

            var response = new SubmitQuestionAnswerResponse
            {
                AnswerId = commandResult.Value,
                QuestionId = request.QuestionId,
                SelectedOptionId = selectedOption.Id,
                AnsweredAt = now
            };

            return Result<SubmitQuestionAnswerResponse>.Success(response);
        }
    }
}
