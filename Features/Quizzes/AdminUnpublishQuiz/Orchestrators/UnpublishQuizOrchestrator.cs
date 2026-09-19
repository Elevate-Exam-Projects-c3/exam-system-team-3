namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrator;

public sealed record UnpublishQuizOrchestrator(Guid QuizId): IRequest<Result<Updated>>;