using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Attempts.GetAttemptHistory.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptHistory.Handlers
{
    public class GetAttemptHistoryQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<GetAttemptHistoryQuery, GetAttemptHistoryResponse> 
    { 
        public async Task<GetAttemptHistoryResponse> Handle(GetAttemptHistoryQuery request, CancellationToken cancellationToken) 
        {
            var attempts = await _attemptRepository
                .Get(x => x.StudentId == request.StudentId && !x.IsDeleted)
                .OrderByDescending(x => x.StartTime)
                .Select(x => new AttemptHistoryItemViewModel
                { 
                    AttemptId = x.Id, QuizId = x.QuizId, QuizTitle = x.Quiz.Title, Status = x.Status, StartTime = x.StartTime,
                    Deadline = x.Deadline, SubmittedAt = x.SubmittedAt, Score = x.Score, Passed = x.Passed })
                .ToListAsync(cancellationToken); 
            return new GetAttemptHistoryResponse { Attempts = attempts }; } }
}
