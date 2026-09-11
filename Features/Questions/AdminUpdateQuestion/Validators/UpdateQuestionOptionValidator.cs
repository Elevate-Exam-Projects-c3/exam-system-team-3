using exam_system.Features.Questions.AdminUpdateQuestion.ViewModels;

namespace exam_system.Features.Questions.AdminUpdateQuestion.Validators
{
    public class UpdateQuestionOptionValidator : AbstractValidator<UpdateQuestionOptionRequest>
    {
        public UpdateQuestionOptionValidator()
        {
            RuleFor(x => x.OptionText)
                .NotEmpty()
                .WithMessage("Option text is required.")
                .MaximumLength(500)
                .WithMessage("Option text cannot exceed 500 characters.");
        }
    }
}
