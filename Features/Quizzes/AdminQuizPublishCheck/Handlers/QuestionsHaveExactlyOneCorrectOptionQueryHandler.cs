using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers;

public class QuestionsHaveExactlyOneCorrectOptionQueryHandler : IRequestHandler<QuestionsHaveExactlyOneCorrectOptionQuery, bool>
{
    public Task<bool> Handle(QuestionsHaveExactlyOneCorrectOptionQuery request, CancellationToken cancellationToken)
    {
        var isValid = request.Quiz.Questions.All(
            question => question.Options.Count(option => option.IsCorrect) == 1);

        return Task.FromResult(isValid);
    }
}
