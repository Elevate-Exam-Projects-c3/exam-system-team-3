using exam_system.Features.Diplomas.GetDiplomaDetail;

namespace exam_system.Features.Diplomas.GetDiplomas.Validation;

public sealed class GetDiplomasQueryValidator : AbstractValidator<GetDiplomasQuery>
{
    public GetDiplomasQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}