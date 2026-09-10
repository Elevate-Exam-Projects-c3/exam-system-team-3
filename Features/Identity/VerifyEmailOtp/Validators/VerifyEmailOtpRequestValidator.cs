using exam_system.Features.Identity.VerifyEmailOtp.Commands;

namespace exam_system.Features.Identity.VerifyEmailOtp.Validators;

public class VerifyEmailOtpCommandValidator : AbstractValidator<VerifyEmailOtpCommand>
{
    public VerifyEmailOtpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("Otp is required.")
            .Length(6).WithMessage("Otp length is invalid.")
            .Matches(@"^\d{6}$").WithMessage("OTP must contain digits only.");
    }

    
}