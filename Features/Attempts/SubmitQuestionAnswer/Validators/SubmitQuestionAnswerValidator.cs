using exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Validators
{
    public sealed class SubmitQuestionAnswerValidator : AbstractValidator<SubmitQuestionAnswerRequest>
    {
        public SubmitQuestionAnswerValidator()
        {
            RuleFor(x => x.SelectedOptionId)
                .NotNull()
                .WithMessage("Selected option is required.");
        }
    }
}
