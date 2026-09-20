using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;
using exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;
using exam_system.Features.Shared.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Handlers;

public class StudentDashboardOrchestratorHandler(IMediator mediator)
    : IRequestHandler<StudentDashboardOrchestrator, Result<StudentDashboardResponse>>
{
    public async Task<Result<StudentDashboardResponse>> Handle(
        StudentDashboardOrchestrator request, CancellationToken cancellationToken)
    {
        var studentId = await mediator.Send(
            new GetStudentIdByUserIdQuery(request.UserId), cancellationToken);

        if (studentId is null)
        {
            return StudentDashboardErrors.StudentNotFound;
        }

        var enrolledDiplomas = await mediator.Send(
            new GetEnrolledDiplomasQuery(studentId.Value), cancellationToken);

        var attemptsSummary = await mediator.Send(
            new GetStudentAttemptsSummaryQuery(studentId.Value), cancellationToken);

        return Result<StudentDashboardResponse>.Success(new StudentDashboardResponse(
            enrolledDiplomas,
            attemptsSummary.RecentAttempts,
            attemptsSummary.AverageScore,
            attemptsSummary.PassRate,
            attemptsSummary.TotalCount,
            attemptsSummary.TotalTimeSpentMinutes));
    }
}