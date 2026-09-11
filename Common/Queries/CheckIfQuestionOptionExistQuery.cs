using exam_system.Domain.Entities.Quizzes;
using exam_system.Persistence.DataAccess;

namespace exam_system.Common.Queries
{
    public record CheckIfQuestionOptionExistQuery(Guid OptionId) : IRequest<bool>;

    public class CheckIfQuestionOptionExistQueryHandler(IGenericRepository<QuestionOption> _optionRepo): IRequestHandler<CheckIfQuestionOptionExistQuery,bool>
    {
        public async Task<bool> Handle(CheckIfQuestionOptionExistQuery request,CancellationToken cancellationToken)
        {
            return await _optionRepo.ExistsAsync(
                x => x.Id == request.OptionId,
                cancellationToken);
        }
    }
}
