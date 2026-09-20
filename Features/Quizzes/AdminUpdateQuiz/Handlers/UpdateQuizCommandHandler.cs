using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Handlers
{
    public class UpdateQuizCommandHandler(IGenericRepository<Quiz> _quizRepo) : IRequestHandler<UpdateQuizCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
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

            await _quizRepo.AddAsync(quiz);
            await _quizRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(quiz.Id);
        }
    }
}
