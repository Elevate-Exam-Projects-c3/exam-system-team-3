using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTO;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Validators
{
    public class UpdateQuizRequestValidator : AbstractValidator<UpdateQuizRequest>
    {
        public UpdateQuizRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3, 200);

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0);

            RuleFor(x => x.PassScore)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.MaxAttempts)
                .GreaterThan(0)
                .When(x => x.MaxAttempts.HasValue);

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .NotEmpty();

            RuleFor(x => x)
                .Must(x => x.EndDate > x.StartDate)
                .WithMessage("End date must be greater than start date.");
        }
    }
}
