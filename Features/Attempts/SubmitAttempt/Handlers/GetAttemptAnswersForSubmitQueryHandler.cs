using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitAttempt.Queries;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public sealed class GetAttemptAnswersForSubmitQueryHandler(IGenericRepository<StudentQuestionAnswer> _answerRepository) : IRequestHandler<GetAttemptAnswersForSubmitQuery,List<AttemptAnswerForSubmit>>
    {
        public async Task<List<AttemptAnswerForSubmit>> Handle(GetAttemptAnswersForSubmitQuery request,CancellationToken cancellationToken)
        {
            return await _answerRepository
                .Get(x =>
                    x.AttemptId == request.AttemptId &&
                    !x.IsDeleted &&
                    x.AnsweredAt <= request.Deadline)
                .Select(x => new AttemptAnswerForSubmit
                {
                    AnswerId = x.Id,
                    SelectedOptionId = x.SelectedOptionId,

                    OptionIsCorrect =
                        x.SelectedOption != null &&
                        x.SelectedOption.IsCorrect,

                    AnsweredAt = x.AnsweredAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
