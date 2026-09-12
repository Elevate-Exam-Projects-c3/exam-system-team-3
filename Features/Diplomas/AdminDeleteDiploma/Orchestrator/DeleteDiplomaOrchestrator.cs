namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Orchestrator;

public sealed record DeleteDiplomaOrchestratorL(Guid DiplomaId) : IRequest<Result<Deleted>>;

