using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Enrollments.EnrollInDiploma.Queries;

namespace exam_system.Features.Enrollments.EnrollInDiploma.Handlers;

public sealed class StudentAlreadyEnrolledQueryHandler(IGenericRepository<StudentEnrollment> enrollmentRepository)
                                                        : IRequestHandler<StudentAlreadyEnrolledQuery, bool>
{
    public async Task<bool> Handle(StudentAlreadyEnrolledQuery request, CancellationToken cancellationToken)
    {
        return await enrollmentRepository
            .Get(enrollment =>
                enrollment.StudentId == request.StudentId &&
                enrollment.DiplomaId == request.DiplomaId &&
                !enrollment.IsDeleted)
            .AnyAsync(cancellationToken);
    }
}