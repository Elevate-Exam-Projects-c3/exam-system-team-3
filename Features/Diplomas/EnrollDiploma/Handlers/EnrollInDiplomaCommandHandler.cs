using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.EnrollDiploma.Quiers;
using exam_system.Features.Enrollments.EnrollInDiploma.Commands;
using Microsoft.Data.SqlClient;

namespace exam_system.Features.Enrollments.EnrollInDiploma.Handlers;

public sealed class EnrollInDiplomaCommandHandler(
                                                  ICurrentUser currentUser, ISender sender,
                                                  IGenericRepository<StudentEnrollment> enrollmentRepository,
                                                  IUnitOfWork unitOfWork)
                                                  : IRequestHandler<EnrollInDiplomaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(EnrollInDiplomaCommand request, CancellationToken cancellationToken)
    {
        var studentId = currentUser.UserId;

        if (studentId is null)
        {
            return EnrollmentErrors.Unauthorized;
        }

        var diplomaAvailabilityResult = await sender.Send(new DiplomaIsAvailableQuiery(request.DiplomaId), cancellationToken);


        if (!diplomaAvailabilityResult)
            return EnrollmentErrors.DiplomaNotAvailable;

        var alreadyEnrolled = await enrollmentRepository
                                                        .Get(enrollment =>
                                                         enrollment.StudentId == studentId.Value && enrollment.DiplomaId == request.DiplomaId)
                                                        .AnyAsync(cancellationToken);

        if (alreadyEnrolled)
            return EnrollmentErrors.AlreadyEnrolled;

        var enrollment = new StudentEnrollment
        {
            StudentId = studentId.Value,
            DiplomaId = request.DiplomaId,
            EnrolledAt = DateTime.UtcNow
        };

        await enrollmentRepository.AddAsync(enrollment);

        try
        {
            // Attempt to save changes to the database 
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception) when (IsUniqueConstraintViolation(exception))
        {
            // If a unique constraint violation occurs, it means the student is already enrolled in the diploma
            return EnrollmentErrors.AlreadyEnrolled;
        }

        // If the save operation is successful, return the ID of the newly created enrollment
        return Result<Guid>.Success(enrollment.Id);
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        return exception.InnerException is SqlException sqlException &&
               sqlException.Number is 2601 or 2627;
    }
}


