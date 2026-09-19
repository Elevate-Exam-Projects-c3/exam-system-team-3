namespace exam_system.Features.Quizzes.AdminPublishQuiz.Orchestrator;

public sealed record PublishQuizOrchestrator(Guid QuizId): IRequest<Result<Updated>>;