using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

namespace exam_system.Features.Quizzes.AdminPublishCheck.Orchestrator;

public sealed class PublishCheckOrchestratorHandler(ISender sender): IRequestHandler<PublishCheckOrchestrator, Result<PublishCheckResponse>>
{
    public async Task<Result<PublishCheckResponse>> Handle(PublishCheckOrchestrator request,CancellationToken cancellationToken)
    {
        var quizResult = await sender.Send(new GetQuizForPublishCheckQuery(request.QuizId),cancellationToken);

        if (quizResult.IsError)return quizResult.TopError;

        var quiz = quizResult.Value;

        var hasQuestions = await sender.Send( new HasQuestionsQuery(quiz),cancellationToken);

        var questionsHaveExactlyOneCorrectOption = await sender.Send( new QuestionsHaveExactlyOneCorrectOptionQuery(quiz),cancellationToken);

        var durationIsValid = await sender.Send( new DurationIsValidQuery(quiz),cancellationToken);

        var passScoreIsValid = await sender.Send( new PassScoreIsValidQuery(quiz),cancellationToken);

        var isReady =hasQuestions &&questionsHaveExactlyOneCorrectOption &&durationIsValid &&passScoreIsValid;

        return new PublishCheckResponse(
            isReady,
            hasQuestions,
            questionsHaveExactlyOneCorrectOption,
            durationIsValid,
            passScoreIsValid);
    }
}