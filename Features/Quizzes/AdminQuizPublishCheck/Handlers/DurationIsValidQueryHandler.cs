using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers;

public class DurationIsValidQueryHandler : IRequestHandler<DurationIsValidQuery, bool>
{
    public Task<bool> Handle(DurationIsValidQuery request, CancellationToken cancellationToken)
    {
        var isValid = request.Data.DurationMinutes > 0;

        return Task.FromResult(isValid);
    }
}
