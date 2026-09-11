namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Queries
{
    public record HasInProgressAttemptsQuery(Guid QuizId) : IRequest<bool>;
}
