namespace exam_system.Features.Diplomas.GetDiplomaDetail.Validation;

public class GetDiplomasQueryValidator : AbstractValidator<GetDiplomasQuery>
{
    public GetDiplomasQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
        RuleFor(x => x.PageSize)
        .InclusiveBetween(1, 100);
    }
}
