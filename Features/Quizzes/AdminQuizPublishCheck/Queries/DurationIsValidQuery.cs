using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

public sealed record DurationIsValidQuery(Quiz Quiz) : IRequest<bool>;