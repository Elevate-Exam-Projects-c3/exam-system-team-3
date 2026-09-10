using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Validators
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull()
                .WithMessage("Create quiz request is required.");

            RuleFor(x => x.Request.DiplomaId)
                .NotEmpty()
                .WithMessage("Diploma is required.");

            RuleFor(x => x.Request.Title)
                .NotEmpty()
                .WithMessage("Quiz title is required.")
                .Length(3, 200)
                .WithMessage("Quiz title must be between 3 and 200 characters.");

            RuleFor(x => x.Request.DurationMinutes)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0.");

            RuleFor(x => x.Request.PassScore)
                .InclusiveBetween(0, 100)
                .WithMessage("Pass score must be between 0 and 100.");

            RuleFor(x => x.Request.MaxAttempts)
                .GreaterThan(0)
                .When(x => x.Request.MaxAttempts.HasValue)
                .WithMessage("Max attempts must be greater than 0.");

            RuleFor(x => x.Request.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.");

            RuleFor(x => x.Request.EndDate)
                .NotEmpty()
                .WithMessage("End date is required.");

            RuleFor(x => x.Request)
                .Must(x => x.EndDate > x.StartDate)
                .WithMessage("End date must be greater than start date.");
        }
    }
}
