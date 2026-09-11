using exam_system.Features.Shared.Results;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Orchestrators
{
    public record DeleteQuizOrchestrator(Guid QuizId) : IRequest<Result<Guid>>;
}
