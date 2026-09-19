using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Queries;

public record GetEnrolledDiplomasQuery(Guid StudentId) : IRequest<IReadOnlyList<EnrolledDiplomaItem>>;