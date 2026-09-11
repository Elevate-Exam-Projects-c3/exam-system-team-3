using exam_system.Features.Questions.AdminCreateQuestion.ViewModels;

namespace exam_system.Features.Questions.AdminCreateQuestion.Validators
{
    public class CreateQuestionOptionValidator : AbstractValidator<CreateQuestionOptionRequest>
    {
        public CreateQuestionOptionValidator()
        {
            RuleFor(x => x.OptionText)
                .NotEmpty()
                .WithMessage("Option text is required.")
                .MaximumLength(1000)
                .WithMessage("Option text cannot exceed 1000 characters.");
        }
    }
}
