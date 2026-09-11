using exam_system.Common.Queries;
using exam_system.Features.Questions.AdminCreateQuestion.Commands;
using exam_system.Features.Questions.AdminCreateQuestion.Orchestrators;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminCreateQuestion.Handlers
{
    public class CreateQuestionOrchestratorHandler(IMediator _mediator) : IRequestHandler<CreateQuestionOrchestrator,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateQuestionOrchestrator request,CancellationToken cancellationToken)
        {
            var exists = await _mediator.Send(new CheckIfQuestionExistQuery(
                    request.QuizId,
                    request.Request.Text),
                cancellationToken);

            if (exists)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUESTION_ALREADY_EXISTS",
                        "A question with this text already exists in the quiz."));
            }

            return await _mediator.Send(
                new CreateQuestionCommand(
                    request.QuizId,
                    request.Request),
                cancellationToken);
        }
    }
}
