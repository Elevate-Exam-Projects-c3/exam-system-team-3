using exam_system.Features.Diplomas.AdminDeleteDiploma.Quiers;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Orchestrator;

public class DeleteDiplomaOrchestratorHandler(ISender sender) : IRequestHandler<DeleteDiplomaOrchestratorL, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteDiplomaOrchestratorL request, CancellationToken cancellationToken)
    {
        var hasActiveEnrollments = await sender.Send(new HasActiveEnrollmentQuery(request.DiplomaId), cancellationToken);

        if (hasActiveEnrollments)
            return DiplomaErrors.HasActiveEnrollments;

        return await sender.Send(new DeleteDiplomaCommand(request.DiplomaId), cancellationToken);
    }
}
