using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class UpdateQuestionOptionCommandHandler(IGenericRepository<QuestionOption> _optionRepo) : IRequestHandler<UpdateQuestionOptionCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuestionOptionCommand request,CancellationToken cancellationToken)
        {
            var option = request.Option;
            var data = request.Request;

            option.OptionText = data.OptionText.Trim();
            option.IsCorrect = data.IsCorrect;

            await _optionRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(option.Id);
        }
    }
}
