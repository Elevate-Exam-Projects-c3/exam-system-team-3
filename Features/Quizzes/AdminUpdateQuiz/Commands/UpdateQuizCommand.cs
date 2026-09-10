using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTO;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Commands
{
    public record UpdateQuizCommand(Quiz Quiz,UpdateQuizRequest Request) : IRequest<Result<Guid>>;
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(UpdateQuizCommand request,CancellationToken cancellationToken)
        {
            var quiz = request.Quiz;
            var data = request.Request;

            quiz.Title = data.Title.Trim();
            quiz.Instructions = data.Instructions?.Trim();
            quiz.DurationMinutes = data.DurationMinutes;
            quiz.PassScore = data.PassScore;
            quiz.MaxAttempts = data.MaxAttempts;
            quiz.StartDate = data.StartDate;
            quiz.EndDate = data.EndDate;

            return Task.FromResult(Result<Guid>.Success(quiz.Id));
        }
    }
}

