using exam_system.Features.Diplomas.EnrollDiploma.Queries;
using exam_system.Features.Enrollments.EnrollInDiploma.Commands;
using exam_system.Features.Enrollments.EnrollInDiploma.Queries;
using exam_system.Features.Shared.Interfaces;

namespace exam_system.Features.Enrollments.EnrollInDiploma.Orchestrator;

public sealed class EnrollInDiplomaOrchestratorHandler(ICurrentUser currentUser, ISender sender)
                                    : IRequestHandler<EnrollInDiplomaOrchestrator, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(EnrollInDiplomaOrchestrator request, CancellationToken cancellationToken)
    {
        var studentId = currentUser.UserId;

        if (studentId is null)
            return EnrollmentErrors.Unauthorized;

        var diplomaIsAvailable = await sender.Send(new DiplomaIsAvailableQuery(request.DiplomaId), cancellationToken);

        if (!diplomaIsAvailable)
            return EnrollmentErrors.DiplomaNotAvailable;

        var alreadyEnrolled = await sender.Send(new StudentAlreadyEnrolledQuery(studentId.Value, request.DiplomaId), cancellationToken);

        if (alreadyEnrolled)
            return EnrollmentErrors.AlreadyEnrolled;

        return await sender.Send(new EnrollInDiplomaCommand(studentId.Value, request.DiplomaId), cancellationToken);
    }
}