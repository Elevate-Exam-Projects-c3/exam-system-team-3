using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.StartAttempt.DTOs;

namespace exam_system.Features.Attempts.StartAttempt.Queries
{
    public record GetQuizForAttemptQuery(Guid QuizId) : IRequest<QuizForAttemptDto?>;
}
