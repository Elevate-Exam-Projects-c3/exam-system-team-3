
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.GetDiplomas.DTOs;
using exam_system.Features.Diplomas.GetDiplomas.Queries;


namespace exam_system.Features.Diplomas.GetDiplomas.Handlers;

public sealed class GetDiplomasQueryHandler(IGenericRepository<Diploma> genericRepository,
    ICurrentUser currentUser) : IRequestHandler<GetDiplomasQuery, Result<PaginatedResult<DiplomaListItemResponse>>>
{

    public async Task<Result<PaginatedResult<DiplomaListItemResponse>>> Handle(
        GetDiplomasQuery request,
        CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not { } studentId)
        {
            return Error.Unauthorized(
                "Student.Unauthorized",
                "Authenticated student identity could not be resolved.");
        }

        var query = genericRepository.GetAll()
            .AsNoTracking()
            .Where(diploma => !diploma.IsDeleted)
            .Where(diploma => diploma.Quizzes.Any(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(diploma => diploma.Title)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(diploma => new DiplomaListItemResponse(
                diploma.Id,
                diploma.Title,
                diploma.Description,
                diploma.Quizzes.Count(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published &&
                quiz.Attempts.Any(attempt => attempt.StudentId == studentId &&
                        (
                            attempt.Status == AttemptStatus.Submitted ||
                            attempt.Status == AttemptStatus.TimedOut
                        ))),
                diploma.Quizzes.Count(quiz => !quiz.IsDeleted && quiz.Status == QuizStatus.Published)))
                .ToListAsync(cancellationToken);

        return new PaginatedResult<DiplomaListItemResponse>(items, totalCount, request.PageIndex, request.PageSize);
    }
}