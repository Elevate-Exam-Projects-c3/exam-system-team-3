namespace exam_system.Features.Enrollments.EnrollInDiploma.Orchestrator;

public sealed record EnrollInDiplomaOrchestrator(Guid DiplomaId) : IRequest<Result<Guid>>;