using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;

public record StudentDashboardOrchestrator(Guid UserId) : IRequest<Result<StudentDashboardResponse>>;