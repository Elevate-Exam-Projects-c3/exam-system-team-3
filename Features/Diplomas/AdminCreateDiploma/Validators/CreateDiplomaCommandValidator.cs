using exam_system.Features.Diplomas.AdminCreateDiploma.Commands;
namespace exam_system.Features.Diplomas.CreateDiploma.Validators;

public sealed class CreateDiplomaCommandValidator
    : AbstractValidator<CreateDiplomaCommand>
{
    public CreateDiplomaCommandValidator()
    {
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