namespace exam_system.Features.Quizzes.AdminPublishQuiz.Commands;


public sealed record PublishQuizCommand(Guid QuizId): IRequest<Result<Updated>>;