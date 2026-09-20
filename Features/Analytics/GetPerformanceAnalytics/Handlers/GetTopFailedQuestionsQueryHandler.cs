using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Analytics.GetPerformanceAnalytics.DTOs.Response;
using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;


namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Handlers;

public class GetTopFailedQuestionsQueryHandler(IGenericRepository<StudentQuestionAnswer> answerRepo)
    : IRequestHandler<GetTopFailedQuestionsQuery, List<TopFailedQuestionItem>>
{
    public async Task<List<TopFailedQuestionItem>> Handle(
        GetTopFailedQuestionsQuery request, CancellationToken cancellationToken)
    {
        var grouped = await answerRepo
            .Get(sqa => sqa.Attempt.SubmittedAt != null
                        && (request.DateFrom == null || sqa.Attempt.SubmittedAt >= request.DateFrom)
                        && (request.DateTo == null || sqa.Attempt.SubmittedAt <= request.DateTo)
                        && (request.DiplomaId == null || sqa.Attempt.Quiz.DiplomaId == request.DiplomaId))
            .GroupBy(sqa => new { sqa.QuestionId, sqa.Question.Text, QuizTitle = sqa.Attempt.Quiz.Title })
            .Select(g => new
            {
                g.Key.QuestionId,
                g.Key.Text,
                g.Key.QuizTitle,
                Total = g.Count(),
                Correct = g.Count(x => x.IsCorrect == true)
            })
            .ToListAsync(cancellationToken);

        return grouped
            .Select(x => new
            {
                x.QuestionId, x.Text, x.QuizTitle, x.Total, x.Correct,
                Rate = x.Total == 0 ? 0 : Math.Round((double)x.Correct / x.Total * 100, 2)
            })
            .Where(x => x.Rate < 40.0)
            .OrderBy(x => x.Rate)
            .Select(x => new TopFailedQuestionItem(x.QuestionId, x.Text, x.QuizTitle, x.Total, x.Correct, x.Rate))
            .ToList();
    }
}