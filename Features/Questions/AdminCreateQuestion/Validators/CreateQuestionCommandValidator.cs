using exam_system.Features.Questions.AdminCreateQuestion.Commands;

namespace exam_system.Features.Questions.AdminCreateQuestion.Validators
{
    public class CreateQuestionCommandValidator
    : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.QuizId)
                .NotEmpty()
                .WithMessage("Quiz is required.");

            RuleFor(x => x.Request.Text)
                .NotEmpty()
                .WithMessage("Question text is required.")
                .MaximumLength(2000)
                .WithMessage("Question text cannot exceed 2000 characters.");

            RuleFor(x => x.Request.Explanation)
                .MaximumLength(5000)
                .When(x => x.Request.Explanation != null)
                .WithMessage("Explanation cannot exceed 5000 characters.");

            RuleFor(x => x.Request.OrderIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Order index cannot be negative.");

            RuleFor(x => x.Request.Options)
                .NotNull()
                .WithMessage("Options are required.");

            RuleFor(x => x.Request.Options)
                .Must(options => options.Count >= 2)
                .WithMessage("A question must have at least 2 options.");

            RuleFor(x => x.Request.Options)
                .Must(options => options.Count(o => o.IsCorrect) == 1)
                .WithMessage("A question must have exactly one correct option.");

            RuleForEach(x => x.Request.Options)
                .SetValidator(new CreateQuestionOptionValidator());
        }
    }
}
