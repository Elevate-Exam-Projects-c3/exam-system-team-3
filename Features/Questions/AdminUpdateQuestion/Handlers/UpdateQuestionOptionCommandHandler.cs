using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public sealed class UpdateQuestionOptionCommandHandler
    : IRequestHandler<
        UpdateQuestionOptionCommand,
        Result<Guid>>
    {
        private readonly IGenericRepository<QuestionOption> _optionRepository;

        public UpdateQuestionOptionCommandHandler(
            IGenericRepository<QuestionOption> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<Result<Guid>> Handle(
            UpdateQuestionOptionCommand request,
            CancellationToken cancellationToken)
        {
            request.Option.OptionText = request.Request.OptionText;
            request.Option.IsCorrect = request.Request.IsCorrect;

            await _optionRepository.SaveChangesAsync(
                cancellationToken);

            return Result<Guid>.Success(request.Option.Id);
        }
    }
}
