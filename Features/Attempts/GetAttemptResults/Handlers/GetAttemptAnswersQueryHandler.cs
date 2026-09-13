using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptResults.Queries;
using exam_system.Features.Attempts.GetAttemptResults.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptResults.Handlers
{
    public class GetAttemptAnswersQueryHandler(IGenericRepository<StudentQuestionAnswer> _answerRepository) : IRequestHandler<GetAttemptAnswersQuery, List<AttemptQuestionResultViewModel>> 
    {
        public async Task<List<AttemptQuestionResultViewModel>> Handle(GetAttemptAnswersQuery request, CancellationToken cancellationToken) 
        {
            return await _answerRepository
                .Get(x => x.AttemptId == request.AttemptId && !x.IsDeleted)
                .OrderBy(x => x.Question.OrderIndex)
                .Select(x => new AttemptQuestionResultViewModel 
                {
                    QuestionId = x.QuestionId, QuestionText = x.Question.Text, SelectedOptionId = x.SelectedOptionId,
                    SelectedOptionText = x.SelectedOption != null ? x.SelectedOption.OptionText : null, 
                    IsCorrect = x.IsCorrect, Explanation = x.Question.Explanation }).ToListAsync(cancellationToken); 
        }
    }
}
