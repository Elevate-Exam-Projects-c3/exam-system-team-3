using exam_system.Domain.Entities.Quizzes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Queries
{
    public record GetQuizQuery(Guid QuizId) : IRequest<Quiz?>;

    
}
