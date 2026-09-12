using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Enrollments.EnrollInDiploma.Commands;
using Microsoft.Data.SqlClient;

namespace exam_system.Features.Enrollments.EnrollInDiploma.Handlers;

public sealed class EnrollInDiplomaCommandHandler(IGenericRepository<StudentEnrollment> enrollmentRepository, IUnitOfWork unitOfWork)
                                                    : IRequestHandler<EnrollInDiplomaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(EnrollInDiplomaCommand request, CancellationToken cancellationToken)
    {
        var enrollment = new StudentEnrollment
        {
            StudentId = request.StudentId,
            DiplomaId = request.DiplomaId,
            EnrolledAt = DateTime.UtcNow
        };

        await enrollmentRepository.AddAsync(enrollment);

        try
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (IsUniqueConstraintViolation(exception))
        {
            return EnrollmentErrors.AlreadyEnrolled;
        }

        return Result<Guid>.Success(enrollment.Id);
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        return exception.InnerException is SqlException sqlException && sqlException.Number is 2601 or 2627;
    }
}