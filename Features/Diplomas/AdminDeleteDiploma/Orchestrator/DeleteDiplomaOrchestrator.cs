namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Orchestrator;

public sealed record DeleteDiplomaOrchestrator(Guid DiplomaId) : IRequest<Result<Deleted>>;

