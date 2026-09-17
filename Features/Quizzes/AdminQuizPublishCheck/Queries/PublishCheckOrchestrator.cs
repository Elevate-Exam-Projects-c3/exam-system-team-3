using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

public sealed record PublishCheckOrchestrator(Guid QuizId): IRequest<Result<PublishCheckResponse>>;
