using exam_system.Features.Identity.RefreshToken.Orchestrators;

namespace exam_system.Features.Identity.RefreshToken.Validators;

public class RefreshTokenOrchestratorValidator : AbstractValidator<RefreshTokenOrchestrator>
{

    public RefreshTokenOrchestratorValidator()
    {
        RuleFor(x => x.RawRefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
    
}