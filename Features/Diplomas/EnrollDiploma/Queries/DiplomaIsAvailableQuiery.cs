namespace exam_system.Features.Diplomas.EnrollDiploma.Quiers;

public sealed record DiplomaIsAvailableQuiery(Guid DiplomaId) : IRequest<bool>;
