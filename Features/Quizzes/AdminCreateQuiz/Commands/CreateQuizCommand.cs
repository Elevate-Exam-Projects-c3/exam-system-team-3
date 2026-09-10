using exam_system.Common.Enums;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminCreateQuiz.ViewModel;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Commands
{
    public record CreateQuizCommand(CreateQuizRequest Request) : IRequest<Result<Guid>>;
    public class CreateQuizCommandHandler(IGenericRepository<Quiz> _quizRepo): IRequestHandler<CreateQuizCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateQuizCommand request,CancellationToken cancellationToken)
        {
            var data = request.Request;

            var quiz = new Quiz
            {
                DiplomaId = data.DiplomaId,
                Title = data.Title.Trim(),
                Instructions = data.Instructions?.Trim(),
                DurationMinutes = data.DurationMinutes,
                PassScore = data.PassScore,
                MaxAttempts = data.MaxAttempts,
                StartDate = data.StartDate,
                EndDate = data.EndDate,

                Status = QuizStatus.Draft
            };

            await _quizRepo.AddAsync(quiz);
            await _quizRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(quiz.Id);
        }
    }
}
