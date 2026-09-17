using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers;

public class PassScoreIsValidQueryHandler : IRequestHandler<PassScoreIsValidQuery, bool>
{
    public Task<bool> Handle(PassScoreIsValidQuery request, CancellationToken cancellationToken)
    {
        var isValid = request.Data.PassScore is >= 0 and <= 100;

        return Task.FromResult(isValid);
    }
}
