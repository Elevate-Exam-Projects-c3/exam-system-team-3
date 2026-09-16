using exam_system.Features.Identity.ForgotPassword.Orchestrators;

namespace exam_system.Features.Identity.ForgotPassword.Validators;

public class ForgotPasswordOrchestratorValidator :AbstractValidator<ForgotPasswordOrchestrator>
{
    public ForgotPasswordOrchestratorValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email formate is Invalid");
    }
}