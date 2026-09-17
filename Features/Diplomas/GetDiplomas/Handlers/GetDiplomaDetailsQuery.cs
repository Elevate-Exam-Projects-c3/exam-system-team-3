using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;
using exam_system.Features.Shared.Interfaces;

namespace exam_system.Features.Diplomas.GetDiplomas.Handlers;

public class GetDiplomaDetailsQueryHandler(IGenericRepository<Diploma> genericRepository, ICurrentUser currentUser)
                                          : IRequestHandler<GetDiplomaDetailsQuery, Result<DiplomaDetailsResponse>>
{
    public async Task<Result<DiplomaDetailsResponse>> Handle(GetDiplomaDetailsQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not { } studentId)
            return Error.Unauthorized("Student.Unauthorized", "Authenticated student identity could not be resolved.");
        var diploma = await genericRepository
           .GetAll()
           .AsNoTracking()
           .Where(diploma => diploma.Id == request.DiplomaId && !diploma.IsDeleted &&
                             diploma.Quizzes.Any(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published))
           .Select(diploma => new DiplomaDetailsResponse(
                   diploma.Id,
                   diploma.Title,
                   diploma.Description,
                   diploma.Quizzes
                   .Where(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published)
                   .Select(quiz => new DiplomaQuizResponse(
                       quiz.Id,
                       quiz.Title,
                       quiz.DurationMinutes,
                       quiz.PassScore,
                       quiz.MaxAttempts,
                       quiz.MaxAttempts == null ||
                       quiz.Attempts.Count(attempt =>
                           attempt.StudentId == studentId &&
                           (attempt.Status == AttemptStatus.Submitted ||
                            attempt.Status == AttemptStatus.TimedOut))
                       < quiz.MaxAttempts.Value,

                       quiz.Attempts.Any(attempt =>
                           attempt.StudentId == studentId &&
                           attempt.Status == AttemptStatus.InProgress)))
                   .ToList())).FirstOrDefaultAsync(cancellationToken);

        if (diploma is null)
            return Error.NotFound("Diploma.NotFound", "Diploma was not found.");

        return diploma;
    }
}
