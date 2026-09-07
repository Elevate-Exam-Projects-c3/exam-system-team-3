using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Validators
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            RuleFor(x => x.DiplomaId)
                .NotEmpty()
                .WithMessage("Diploma is required.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Quiz title is required.")
                .Length(3, 200)
                .WithMessage("Quiz title must be between 3 and 200 characters.");

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0.");

            RuleFor(x => x.PassScore)
                .InclusiveBetween(0, 100)
                .WithMessage("Pass score must be between 0 and 100.");

            RuleFor(x => x.MaxAttempts)
                .GreaterThan(0)
                .When(x => x.MaxAttempts.HasValue)
                .WithMessage("Max attempts must be greater than 0.");
        }
    }
}
