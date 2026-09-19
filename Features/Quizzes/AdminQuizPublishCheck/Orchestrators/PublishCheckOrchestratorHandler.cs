using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Services;

namespace exam_system.Features.Quizzes.AdminPublishCheck.Orchestrator;

public sealed class PublishCheckOrchestratorHandler(ISender sender):IRequestHandler<PublishCheckOrchestrator, Result<PublishCheckResponse>>
{
    public async Task<Result<PublishCheckResponse>> Handle(PublishCheckOrchestrator request,CancellationToken cancellationToken)
    {
        var quizResult = await sender.Send( new GetQuizForPublishCheckQuery(request.QuizId),cancellationToken);

        if (quizResult.IsError)
            return quizResult.TopError;

        var publishCheck = PublishCheckEvaluator.Evaluate(quizResult.Value);

        return publishCheck;
    }
}