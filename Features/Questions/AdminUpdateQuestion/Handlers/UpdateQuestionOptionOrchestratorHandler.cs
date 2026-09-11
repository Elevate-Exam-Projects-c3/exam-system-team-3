using exam_system.Common.Enums;
using exam_system.Features.Questions.AdminUpdateQuestion.Commands;
using exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Queries;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class UpdateQuestionOptionOrchestratorHandler(IMediator _mediator) : IRequestHandler<UpdateQuestionOptionOrchestrator,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuestionOptionOrchestrator request,CancellationToken cancellationToken)
        {
            // 1. Get option
            var option = await _mediator.Send(new GetQuestionOptionQuery(request.OptionId),cancellationToken);

            if (option is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_OPTION_NOT_FOUND",
                        "Question option not found."));
            }

            // 2. Make sure option belongs to question
            if (option.QuestionId != request.QuestionId)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_OPTION_NOT_FOUND",
                        "Question option not found in this question."));
            }

            // 3. Make sure question belongs to quiz
            if (option.Question.QuizId != request.QuizId)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_OPTION_NOT_FOUND",
                        "Question option not found in this quiz."));
            }

            // 4. Published quiz guard
            //
            // You need the Quiz loaded here.
            // If Question.Quiz isn't loaded, use a GetQuizQuery.

            var quiz = await _mediator.Send(
                new GetQuizQuery(option.Question.QuizId),
                cancellationToken);

            if (quiz is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUIZ_NOT_FOUND",
                        "Quiz not found."));
            }

            if (quiz.Status == QuizStatus.Published)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUIZ_IS_PUBLISHED",
                        "Question options cannot be modified while the quiz is published. Unpublish the quiz first."));
            }

            // 5. Get all options
            var options = await _mediator.Send(new GetQuestionOptionsQuery(
                    request.QuestionId),
                cancellationToken);

            // 6. Validate resulting correct-option count
            var correctCount = options.Count(x =>
                x.Id == request.OptionId
                    ? request.Request.IsCorrect
                    : x.IsCorrect);

            if (correctCount != 1)
            {
                return Result<Guid>.Failure(
                    Error.Validation(
                        "INVALID_CORRECT_OPTION_COUNT",
                        "A question must have exactly one correct option."));
            }

            // 7. Update
            return await _mediator.Send(
                new UpdateQuestionOptionCommand(
                    option,
                    request.Request),
                cancellationToken);
        }
    }
}
