using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminCreateQuiz.Queries;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTO;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators
{
    public class UpdateQuizOrchestrator(IMediator _mediator)
    {
        public async Task<Result<Guid>> ExecuteAsync(Guid quizId,UpdateQuizRequest request,CancellationToken cancellationToken)
        {
            var exists = await _mediator.Send(
                new CheckIfExistQuery(request.Title, quizId),
                cancellationToken);

            if (exists)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUIZ_ALREADY_EXISTS",
                        "A quiz with this title already exists."));
            }

            var quiz = await _mediator.Send(
                new GetQuizQuery(quizId),
                cancellationToken);

            if (quiz is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUIZ_NOT_FOUND",
                        "Quiz not found."));
            }

            return await _mediator.Send(new UpdateQuizCommand(quiz,request),cancellationToken);
        }
    }
}


