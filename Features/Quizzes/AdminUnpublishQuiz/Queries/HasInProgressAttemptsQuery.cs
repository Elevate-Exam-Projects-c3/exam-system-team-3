namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries;

public sealed record HasInProgressAttemptsQuery(Guid QuizId):IRequest<bool>;