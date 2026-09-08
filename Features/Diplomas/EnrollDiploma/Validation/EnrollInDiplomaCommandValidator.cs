using exam_system.Features.Enrollments.EnrollInDiploma.Commands;

namespace exam_system.Features.Diplomas.EnrollDiploma.Validation;

public class EnrollInDiplomaCommandValidator : AbstractValidator<EnrollInDiplomaCommand>
{
    public EnrollInDiplomaCommandValidator()
    {
        RuleFor(x => x.DiplomaId)
            .NotEmpty();
    }
}
