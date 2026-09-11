using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminUpdateQuestion.Commands;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Handlers
{
    public class UpdateQuestionCommandHandler(IGenericRepository<Question> _questionRepo): IRequestHandler<UpdateQuestionCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuestionCommand request,CancellationToken cancellationToken)
        {
            var question = request.Question;
            var data = request.Request;

            question.Text = data.Text.Trim();
            question.Explanation = data.Explanation?.Trim();
            question.OrderIndex = data.OrderIndex;

            // Update existing / add new options
            foreach (var optionRequest in data.Options)
            {
                if (optionRequest.Id.HasValue)
                {
                    var existingOption = question.Options
                        .FirstOrDefault(x =>
                            x.Id == optionRequest.Id.Value);

                    if (existingOption is null)
                    {
                        return Result<Guid>.Failure(
                            Error.NotFound(
                                "QUESTION_OPTION_NOT_FOUND",
                                "Question option not found."));
                    }

                    existingOption.OptionText =
                        optionRequest.OptionText.Trim();

                    existingOption.IsCorrect =
                        optionRequest.IsCorrect;
                }
                else
                {
                    question.Options.Add(
                        new QuestionOption
                        {
                            OptionText =
                                optionRequest.OptionText.Trim(),

                            IsCorrect =
                                optionRequest.IsCorrect
                        });
                }
            }

            await _questionRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(question.Id);
        }
    }
}
