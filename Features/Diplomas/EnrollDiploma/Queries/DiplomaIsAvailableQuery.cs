namespace exam_system.Features.Diplomas.EnrollDiploma.Queries;

public sealed record DiplomaIsAvailableQuery(Guid DiplomaId) : IRequest<bool>;