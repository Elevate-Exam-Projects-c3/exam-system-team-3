using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Queries;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class GetQuestionOptionsQueryHandler(IGenericRepository<QuestionOption> _optionRepository) : IRequestHandler<GetQuestionOptionsQuery, List<QuestionOption>> 
    {
        public async Task<List<QuestionOption>> Handle(GetQuestionOptionsQuery request, CancellationToken cancellationToken) 
        {
            return await _optionRepository
                .Get(x => x.QuestionId == request.QuestionId && !x.IsDeleted)
                .ToListAsync(cancellationToken);
        }
    }
}
