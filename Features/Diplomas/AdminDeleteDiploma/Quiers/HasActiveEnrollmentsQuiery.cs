namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Quiers;

public sealed record HasActiveEnrollmentsQuiery(Guid DiplomaId) : IRequest<bool>;
