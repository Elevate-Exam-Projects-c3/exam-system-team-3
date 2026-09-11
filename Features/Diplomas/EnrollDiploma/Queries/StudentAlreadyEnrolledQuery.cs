namespace exam_system.Features.Enrollments.EnrollInDiploma.Queries;

public sealed record StudentAlreadyEnrolledQuery(Guid StudentId, Guid DiplomaId) : IRequest<bool>;