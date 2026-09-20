using exam_system.Features.Attempts.SubmitAttempt.ViewModels;

namespace exam_system.Features.Attempts.SubmitAttempt.Shared
{
    public interface IAttemptFinalizationService
    {
        Task<Result<SubmitQuizAttemptResponse>> SubmitAsync(
            Guid attemptId,
            CancellationToken cancellationToken);

        Task<Result<SubmitQuizAttemptResponse>> TimeoutAsync(
            Guid attemptId,
            CancellationToken cancellationToken);
    }
}
