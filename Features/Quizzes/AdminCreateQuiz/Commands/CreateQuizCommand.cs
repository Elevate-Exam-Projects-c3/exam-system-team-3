using exam_system.Common.Enums;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Commands
{
    public record CreateQuizCommand(Guid DiplomaId, string Title, string? Instructions, int DurationMinutes, int PassScore, int? MaxAttempts) : IRequest<Guid>;

    public class CreateQuizCommandHandler (IGenericRepository<Quiz> _quizRepo) : IRequestHandler<CreateQuizCommand, Guid>
    {
        public async Task<Guid> Handle(CreateQuizCommand request,CancellationToken cancellationToken)
        {
            var quiz = new Quiz
            {
                DiplomaId = request.DiplomaId,
                Title = request.Title,
                Instructions = request.Instructions,
                DurationMinutes = request.DurationMinutes,
                PassScore = request.PassScore,
                MaxAttempts = request.MaxAttempts,
                Status = QuizStatus.Draft
            };

            await _quizRepo.AddAsync(quiz);

            await _quizRepo.SaveChangesAsync(
                cancellationToken);

            return quiz.Id;
        }
    }
}
