using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptResults.Queries;
using exam_system.Features.Attempts.GetAttemptResults.ViewModels;

namespace exam_system.Features.Attempts.GetAttemptResults.Handlers
{
    public class GetAttemptForResultsQueryHandler(IGenericRepository<QuizAttempt> _attemptRepository) : IRequestHandler<GetAttemptForResultsQuery, AttemptResultAttemptViewModel?> 
    {
        public async Task<AttemptResultAttemptViewModel?> Handle(GetAttemptForResultsQuery request, CancellationToken cancellationToken)
        {
            return await _attemptRepository
                .Get(x => x.Id == request.AttemptId && x.StudentId == request.StudentId && !x.IsDeleted)
                .Select(x => new AttemptResultAttemptViewModel 
                {
                    AttemptId = x.Id, QuizId = x.QuizId, QuizTitle = x.Quiz.Title, Status = x.Status, StartTime = x.StartTime, SubmittedAt = x.SubmittedAt, 
                    Score = x.Score, Passed = x.Passed })
                .FirstOrDefaultAsync(cancellationToken); 
        }
    }
}
