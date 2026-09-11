using exam_system.Common.Queries;
using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Handlers
{
    public class CreateQuizOrchestratorHandler(IMediator _mediator) : IRequestHandler<CreateQuizOrchestrator, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateQuizOrchestrator request,CancellationToken cancellationToken)
        {
            var exists = await _mediator.Send(
                new CheckIfExistQuery(request.Request.Title),
                cancellationToken);

            if (exists)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUIZ_ALREADY_EXISTS",
                        "A quiz with this title already exists."));
            }

            return await _mediator.Send(new CreateQuizCommand(request.Request),cancellationToken);
        }
    }
}
