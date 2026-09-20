namespace exam_system.Features.Attempts.SubmitAttempt.Commands
{
    public record SubmitQuizAttemptCommand(Guid AttemptId,double Score,bool Passed,DateTime SubmittedAt,AttemptStatus Status,Dictionary<Guid, bool> AnswerCorrectness) : IRequest<Result<Guid>>;   
    //The dictionary means:  AnswerId → calculated IsCorrect
}
