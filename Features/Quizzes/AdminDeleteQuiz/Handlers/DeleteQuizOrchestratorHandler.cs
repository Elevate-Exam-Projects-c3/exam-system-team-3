using exam_system.Common.Enums;
using exam_system.Common.Queries;
using exam_system.Features.Quizzes.AdminDeleteQuiz.Commands;
using exam_system.Features.Quizzes.AdminDeleteQuiz.Orchestrators;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Handlers
{
    public class DeleteQuizOrchestratorHandler(IMediator _mediator) : IRequestHandler<DeleteQuizOrchestrator,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(DeleteQuizOrchestrator request,CancellationToken cancellationToken)
        {
            var quiz = await _mediator.Send( new GetQuizQuery(request.QuizId),cancellationToken);

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
                        "A published quiz cannot be deleted. Unpublish the quiz first."));
            }

            return await _mediator.Send(new DeleteQuizCommand(quiz),cancellationToken);
        }
    }
}
