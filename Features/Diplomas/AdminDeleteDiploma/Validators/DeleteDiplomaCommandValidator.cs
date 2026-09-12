namespace exam_system.Features.Diplomas.DeleteDiploma.Validators;

public sealed class DeleteDiplomaCommandValidator
    : AbstractValidator<DeleteDiplomaCommand>
{
    public DeleteDiplomaCommandValidator()
    {
        RuleFor(x => x.DiplomaId)
            .NotEmpty();
    }
}