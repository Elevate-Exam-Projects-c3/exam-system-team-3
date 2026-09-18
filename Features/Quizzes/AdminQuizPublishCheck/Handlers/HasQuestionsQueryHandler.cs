using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers;

public class HasQuestionsQueryHandler : IRequestHandler<HasQuestionsQuery, bool>
{
    public Task<bool> Handle(HasQuestionsQuery request, CancellationToken cancellationToken)
    {
        var hasQuestions = request.Data.QuestionsCount > 0;
        return Task.FromResult(hasQuestions);
    }
}
