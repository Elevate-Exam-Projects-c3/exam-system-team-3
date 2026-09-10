using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;

namespace exam_system.Features.Diplomas.DeleteDiploma.Validators;

public sealed class DeleteDiplomaCommandValidator
    : AbstractValidator<DeleteDiplomaCommand>
{
    public DeleteDiplomaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}