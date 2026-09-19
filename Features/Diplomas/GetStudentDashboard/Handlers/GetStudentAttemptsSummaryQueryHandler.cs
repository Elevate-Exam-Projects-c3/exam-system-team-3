using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Internal;
using exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Handlers;

public class GetStudentAttemptsSummaryQueryHandler(
    IGenericRepository<QuizAttempt> attemptRepo)
    : IRequestHandler<GetStudentAttemptsSummaryQuery, StudentAttemptsSummaryResult>
{
    public async Task<StudentAttemptsSummaryResult> Handle(
        GetStudentAttemptsSummaryQuery request, CancellationToken cancellationToken)
    {
        var allAttempts = await attemptRepo
            .Get(a => a.StudentId == request.StudentId && a.SubmittedAt != null)
            .OrderByDescending(a => a.SubmittedAt)
            .Select(a => new
            {
                a.Id,
                a.QuizId,
                QuizTitle = a.Quiz.Title,
                a.Score,
                a.Passed,
                a.SubmittedAt,
                a.StartTime
            })
            .ToListAsync(cancellationToken);

        var recentAttempts = allAttempts
            .Take(5)
            .Select(a => new RecentAttemptItem(a.Id, a.QuizId, a.QuizTitle, a.Score, a.Passed, a.SubmittedAt))
            .ToList();

        var averageScore = allAttempts.Count > 0 ? allAttempts.Average(a => a.Score ?? 0) : 0;

        var passRate = allAttempts.Count > 0
            ? (double)allAttempts.Count(a => a.Passed == true) / allAttempts.Count * 100
            : 0;

        var totalTimeSpentMinutes = allAttempts.Sum(a =>
            (a.SubmittedAt!.Value - a.StartTime).TotalMinutes);

        return new StudentAttemptsSummaryResult(
            recentAttempts,
            Math.Round(averageScore, 2),
            Math.Round(passRate, 2),
            allAttempts.Count,
            Math.Round(totalTimeSpentMinutes, 2));
    }
}