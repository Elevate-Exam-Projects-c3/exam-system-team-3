using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Internal;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Queries;

public record GetStudentAttemptsSummaryQuery(Guid StudentId) : IRequest<StudentAttemptsSummaryResult>;