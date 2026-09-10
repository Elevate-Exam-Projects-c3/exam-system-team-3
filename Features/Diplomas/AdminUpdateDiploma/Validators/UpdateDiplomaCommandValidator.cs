
using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;

namespace exam_system.Features.Diplomas.UpdateDiploma.Validators;

public sealed class UpdateDiplomaCommandValidator: AbstractValidator<UpdateDiplomaCommand>
{
    public UpdateDiplomaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}