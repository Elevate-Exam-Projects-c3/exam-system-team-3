using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.StartAttempt.Commands;
using exam_system.Features.Attempts.StartAttempt.DTOs;
using exam_system.Features.Attempts.StartAttempt.Orchestrators;
using exam_system.Features.Attempts.StartAttempt.Queries;
using exam_system.Features.Attempts.StartAttempt.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class StartAttemptOrchestratorHandler(IMediator _mediator): IRequestHandler<StartAttemptOrchestrator,Result<StartAttemptResponse>>
    {
        public async Task<Result<StartAttemptResponse>> Handle(StartAttemptOrchestrator request,CancellationToken cancellationToken)
        {
            // =====================================================
            // 1. Get quiz
            // =====================================================

            var quiz = await _mediator.Send(new GetQuizForAttemptQuery(request.QuizId),cancellationToken);

            if (quiz is null)
            {
                return Result<StartAttemptResponse>.Failure(
                    Error.NotFound(
                        "QUIZ_NOT_FOUND",
                        "Quiz was not found."));
            }

            // =====================================================
            // 2. Quiz must be published
            // =====================================================

            if (quiz.Status != QuizStatus.Published)
            {
                return Result<StartAttemptResponse>.Failure(
                    Error.Conflict(
                        "QUIZ_NOT_AVAILABLE",
                        "This quiz is not currently available."));
            }

            // =====================================================
            // 3. Check quiz date/time availability
            // =====================================================

            var now = DateTime.UtcNow;

            if (now < quiz.StartDate || now > quiz.EndDate)
            {
                return Result<StartAttemptResponse>.Failure(
                    Error.Conflict(
                        "QUIZ_OUTSIDE_AVAILABLE_PERIOD",
                        "This quiz is not currently available."));
            }

            // =====================================================
            // 4. Check existing in-progress attempt
            // =====================================================

            var existingAttempt = await _mediator.Send(new GetInProgressAttemptQuery(request.StudentId,request.QuizId),cancellationToken);

            if (existingAttempt is not null)
            {
                return BuildResponse(
                    existingAttempt,
                    quiz);
            }

            // =====================================================
            // 5. Check MaxAttempts
            // =====================================================

            if (quiz.MaxAttempts.HasValue)
            {
                var attemptCount = await _mediator.Send(new GetAttemptCountQuery(request.StudentId,request.QuizId),cancellationToken);

                if (attemptCount >= quiz.MaxAttempts.Value)
                {
                    return Result<StartAttemptResponse>.Failure(
                        Error.Conflict(
                            "ATTEMPTS_EXHAUSTED",
                            "You have reached the maximum number of attempts for this quiz."));
                }
            }

            // =====================================================
            // 6. Create attempt
            // =====================================================

            var createResult = await _mediator.Send(new StartAttemptCommand(request.StudentId,request.QuizId,quiz.DurationMinutes),cancellationToken);

            if (!createResult.IsSuccess)
            {
                return Result<StartAttemptResponse>.Failure(
                    createResult.Errors[0]);
            }

            // =====================================================
            // 7. Get created attempt
            // =====================================================

            var attempt = await _mediator.Send(new GetInProgressAttemptQuery(request.StudentId,request.QuizId),cancellationToken);

            if (attempt is null)
            {
                return Result<StartAttemptResponse>.Failure(
                    Error.Unexpected(
                        "ATTEMPT_CREATE_FAILED",
                        "The attempt was created but could not be loaded."));
            }

            // =====================================================
            // 8. Build response
            // =====================================================

            return BuildResponse(
                attempt,
                quiz);
        }

        private static Result<StartAttemptResponse> BuildResponse(QuizAttempt attempt,QuizForAttemptDto quiz)
        {
            var questions = quiz.Questions
                .Select((question, index) =>
                    new AttemptQuestionResponse
                    {
                        Id = question.Id,

                        Text = question.QuestionText,

                        DisplayOrder = index + 1,

                        Options = question.Options
                            .Select((option, optionIndex) =>
                                new AttemptOptionResponse
                                {
                                    Id = option.Id,

                                    OptionText = option.OptionText,

                                    DisplayOrder = optionIndex + 1
                                })
                            .ToList()
                    })
                .ToList();

            return Result<StartAttemptResponse>.Success(
                new StartAttemptResponse
                {
                    AttemptId = attempt.Id,
                    StartTime = attempt.StartTime,
                    Deadline = attempt.Deadline,
                    Questions = questions
                });
        }

        
    }
}
