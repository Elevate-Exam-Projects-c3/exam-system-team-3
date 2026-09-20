using exam_system.Domain.Entities.Attempts;

namespace exam_system.Features.Attempts.SubmitAttempt.Queries
{
    public record GetAttemptAnswersForSubmitQuery(Guid AttemptId,DateTime Deadline) : IRequest<List<AttemptAnswerForSubmit>>;    
    //But I recommend not returning entities with navigation properties here.
}
