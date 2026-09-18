using exam_system.Features.Questions.AdminUpdateQuestion.Commands;
using exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public sealed class UpdateQuestionOptionOrchestratorHandler(IMediator _mediator) : IRequestHandler<UpdateQuestionOptionOrchestrator,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuestionOptionOrchestrator request,CancellationToken cancellationToken)
        {
            // 1. Get question
            var question = await _mediator.Send(new GetQuestionQuery(request.QuestionId),cancellationToken);

            if (question is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "Question was not found."));
            }

            // 2. Verify question belongs to quiz
            if (question.QuizId != request.QuizId)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "Question was not found in this quiz."));
            }

            // 3. Get option
            var option = await _mediator.Send(
                new GetQuestionOptionForUpdateQuery(
                    request.OptionId),
                cancellationToken);

            if (option is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "OPTION_NOT_FOUND",
                        "Question option was not found."));
            }

            // 4. Verify option belongs to question
            if (option.QuestionId != request.QuestionId)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "OPTION_NOT_FOUND",
                        "Question option was not found for this question."));
            }

            // 5. Send command
            return await _mediator.Send(new UpdateQuestionOptionCommand(
                    option,
                    request.Request),
                cancellationToken);
        }
    }
}
