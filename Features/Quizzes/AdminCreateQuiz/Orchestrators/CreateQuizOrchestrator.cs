using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Queries;
using exam_system.Features.Quizzes.AdminCreateQuiz.ViewModel;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators
{
    public class CreateQuizOrchestrator(IMediator _mediator)
    {
        public async Task<Result<Guid>> ExecuteAsync(CreateQuizRequest request,CancellationToken cancellationToken)
        {
            var exists = await _mediator.Send(new CheckIfExistQuery(request.Title),
                cancellationToken);

            if (exists)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUIZ_ALREADY_EXISTS",
                        "A quiz with this title already exists."));
            }


            var command = new CreateQuizCommand(request);

            return await _mediator.Send(command,cancellationToken);
        }
    }
}
