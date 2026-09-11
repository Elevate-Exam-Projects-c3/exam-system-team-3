using exam_system.Features.Identity.Login.Orchestrators;
using FluentValidation;

namespace exam_system.Features.Identity.Login.Validators;

public class LoginOrchestratorValidator
    : AbstractValidator<LoginOrchestrator>
{
    public LoginOrchestratorValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}