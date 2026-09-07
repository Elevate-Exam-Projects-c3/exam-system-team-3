using exam_system.Common.Enums;
using exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;
using exam_system.Features.Shared;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Handlers;

public sealed class GetDiplomasQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetDiplomasQuery,Result<PaginatedResult<DiplomaListItemResponse>>>
{
    public async Task<Result<PaginatedResult<DiplomaListItemResponse>>> Handle(GetDiplomasQuery request,CancellationToken cancellationToken)
    {
        //As a Student, I want to browse all published diploma programs, so that I can decide which one to pursue.
        var query = dbContext.Diplomas
            .AsNoTracking()
            .Where(diploma =>diploma.Quizzes.Any(quiz =>quiz.Status == QuizStatus.Published));

        var totalCount = await query.CountAsync(cancellationToken);
        
        var items = await query
                    .OrderBy(diploma => diploma.Title)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)

               .Select(diploma => new DiplomaListItemResponse
               {
                   Id = diploma.Id,
                   Title = diploma.Title,
                   Description = diploma.Description,

                   CompletedQuizzes = diploma.Quizzes.Count(quiz =>
                       quiz.Status == QuizStatus.Published &&
                       quiz.Attempts.Any(attempt =>
                           attempt.StudentId == request.StudentId &&
                           (
                               attempt.Status == AttemptStatus.Submitted ||
                               attempt.Status == AttemptStatus.TimedOut
                           ))),

                   TotalQuizzes = diploma.Quizzes.Count(quiz =>
                       quiz.Status == QuizStatus.Published)
               })
            .ToListAsync(cancellationToken);

        return new PaginatedResult<DiplomaListItemResponse>(items,totalCount,request.PageIndex,request.PageSize);
    }
}