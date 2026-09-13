using exam_system.Features.Attempts.GetAttemptResults.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptResults.Queries
{
    public record GetAttemptAnswersQuery(Guid AttemptId) : IRequest<List<AttemptQuestionResultViewModel>>;
}
