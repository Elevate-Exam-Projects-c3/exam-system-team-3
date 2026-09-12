namespace exam_system.Features.Enrollments.EnrollInDiploma.Commands;

public sealed record EnrollInDiplomaCommand(Guid StudentId, Guid DiplomaId) : IRequest<Result<Guid>>;