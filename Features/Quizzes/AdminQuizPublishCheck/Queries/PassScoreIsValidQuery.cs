using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

public sealed record PassScoreIsValidQuery(PublishCheckData Data) : IRequest<bool>;