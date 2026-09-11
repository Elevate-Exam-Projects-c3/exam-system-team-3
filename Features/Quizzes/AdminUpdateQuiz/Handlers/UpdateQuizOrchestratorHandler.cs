using exam_system.Common.Queries;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators.exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Queries;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Handlers
{
    public class UpdateQuizOrchestratorHandler(IMediator _mediator) : IRequestHandler<UpdateQuizOrchestrator, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuizOrchestrator request,CancellationToken cancellationToken)
        {
            var exists = await _mediator.Send(
                new CheckIfExistQuery(
                    request.Request.Title,
                    request.QuizId),
                cancellationToken);

            if (exists)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUIZ_ALREADY_EXISTS",
                        "A quiz with this title already exists."));
            }

            var quiz = await _mediator.Send(
                new GetQuizQuery(request.QuizId),
                cancellationToken);

            if (quiz is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUIZ_NOT_FOUND",
                        "Quiz not found."));
            }

            return await _mediator.Send(new UpdateQuizCommand(quiz,request.Request),cancellationToken);
        }
    }
}
