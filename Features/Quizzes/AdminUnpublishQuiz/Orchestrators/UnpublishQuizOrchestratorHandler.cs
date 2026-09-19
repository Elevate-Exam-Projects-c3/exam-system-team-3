using exam_system.Features.Quizzes.AdminUnpublishQuiz.Commands;
using exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrator;

public sealed class UnpublishQuizOrchestratorHandler(ISender sender): IRequestHandler<UnpublishQuizOrchestrator, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UnpublishQuizOrchestrator request,CancellationToken cancellationToken)
    {
        var hasInProgressAttempts = await sender.Send(new HasInProgressAttemptsQuery(request.QuizId),cancellationToken);

        if (hasInProgressAttempts)
            return UnpublishQuizErrors.HasInProgressAttempts;

        return await sender.Send(new UnpublishQuizCommand(request.QuizId),cancellationToken);
    }
}