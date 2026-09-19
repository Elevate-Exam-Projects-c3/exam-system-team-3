using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Handlers;

public class GetEnrolledDiplomasQueryHandler(
    IGenericRepository<StudentEnrollment> enrollmentRepo)
    : IRequestHandler<GetEnrolledDiplomasQuery, IReadOnlyList<EnrolledDiplomaItem>>
{
    public async Task<IReadOnlyList<EnrolledDiplomaItem>> Handle(
        GetEnrolledDiplomasQuery request, CancellationToken cancellationToken)
    {
        return await enrollmentRepo
            .Get(e => e.StudentId == request.StudentId)
            .OrderByDescending(e => e.EnrolledAt)
            .Select(e => new EnrolledDiplomaItem(e.DiplomaId, e.Diploma.Title, e.EnrolledAt))
            .ToListAsync(cancellationToken);
    }
}