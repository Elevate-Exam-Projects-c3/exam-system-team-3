using exam_system.Common.Enums;
using exam_system.Common.Interfaces;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Enrollments.EnrollInDiploma.Commands;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Enrollments.EnrollInDiploma.Handlers;

public sealed class EnrollInDiplomaCommandHandler(
                                                  ICurrentUser currentUser,
                                                  IGenericRepository<Diploma> diplomaRepository,
                                                  IGenericRepository<StudentEnrollment> enrollmentRepository,
                                                  IUnitOfWork unitOfWork)
                                                  : IRequestHandler<EnrollInDiplomaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(EnrollInDiplomaCommand request,CancellationToken cancellationToken)
    {
        var studentId = currentUser.UserId;

        if (studentId is null)
        {
            return EnrollmentErrors.Unauthorized;
        }
        //أول Business Rule: التأكد أن الـ Diploma متاح للتسجيل
        var diplomaIsAvailable = await diplomaRepository
                                                        .Get(diploma =>
                                                        diploma.Id == request.DiplomaId &&!diploma.IsDeleted &&
                                                        diploma.Quizzes.Any(quiz =>!quiz.IsDeleted &&quiz.Status == QuizStatus.Published))
                                                        .AnyAsync(cancellationToken);

        if (!diplomaIsAvailable) 
            return EnrollmentErrors.DiplomaNotAvailable;

        //بعد ذلك نفحص Business Rule الثانية: هل الطالب مسجل مسبقًا؟
        var alreadyEnrolled = await enrollmentRepository
                                                        .Get(enrollment =>
                                                         enrollment.StudentId == studentId.Value &&enrollment.DiplomaId == request.DiplomaId)
                                                        .AnyAsync(cancellationToken);

        if (alreadyEnrolled)
            return EnrollmentErrors.AlreadyEnrolled;

        // إذا اجتاز الطالب جميع الـ Business Rules، نقوم بإنشاء سجل جديد في جدول StudentEnrollment
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
        catch (DbUpdateException exception)when (IsUniqueConstraintViolation(exception))
        {
            // If a unique constraint violation occurs, it means the student is already enrolled in the diploma
            return EnrollmentErrors.AlreadyEnrolled;
        }

        // If the save operation is successful, return the ID of the newly created enrollment
        // ولو رجع بالفعل كده الطالب تم التسجيل بالفعل ونجحت العملية 
        //وانا متاكد انه لو مر من Try فوق انه كده هيكون نجح بالفعل لان لو في خطاء في الدتا بيز هيدخل الCatch ويطلع AlreadyEnrolled
        return enrollment.Id;
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        return exception.InnerException is SqlException sqlException &&
               sqlException.Number is 2601 or 2627;
    }
}