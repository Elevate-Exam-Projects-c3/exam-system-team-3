namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Quiers;

public sealed record HasActiveEnrollmentQuery(Guid DiplomaId) : IRequest<bool>;
