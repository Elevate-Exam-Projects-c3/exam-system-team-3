using exam_system.Common.Queries;
using exam_system.Features.Questions.AdminUpdateQuestion.Commands;
using exam_system.Features.Questions.AdminUpdateQuestion.Orchestrators;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class UpdateQuestionOrchestratorHandler(IMediator _mediator) : IRequestHandler<UpdateQuestionOrchestrator,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuestionOrchestrator request,CancellationToken cancellationToken)
        {
            var exists = await _mediator.Send(
                new CheckIfQuestionExistQuery(
                    request.QuizId,
                    request.Request.Text,
                    request.QuestionId),
                cancellationToken);

            if (exists)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUESTION_ALREADY_EXISTS",
                        "A question with this text already exists in the quiz."));
            }

            var question = await _mediator.Send(
                new GetQuestionQuery(request.QuestionId),
                cancellationToken);

            if (question is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "Question not found."));
            }

            // 3. Make sure question belongs to quiz
            if (question.QuizId != request.QuizId)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "Question not found in this quiz."));
            }

            // 4. Update
            return await _mediator.Send(new UpdateQuestionCommand(question,request.Request),cancellationToken);
        }
    }
}
