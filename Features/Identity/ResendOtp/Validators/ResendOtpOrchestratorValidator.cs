using exam_system.Features.Identity.ResendOtp.Orchestrators;

namespace exam_system.Features.Identity.ResendOtp.Validators;

public class ResendOtpOrchestratorValidator : AbstractValidator<ResendOtpOrchestrator>
{
    public ResendOtpOrchestratorValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");
    }
}