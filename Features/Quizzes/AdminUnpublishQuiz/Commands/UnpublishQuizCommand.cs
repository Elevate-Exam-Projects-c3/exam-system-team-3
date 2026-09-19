namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Commands;

public sealed record UnpublishQuizCommand(Guid QuizId): IRequest<Result<Updated>>;