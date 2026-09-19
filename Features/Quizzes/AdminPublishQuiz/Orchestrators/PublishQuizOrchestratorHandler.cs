using exam_system.Features.Quizzes.AdminPublishQuiz.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Services;
using exam_system.Features.Shared.Results.ErrorCodes;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Orchestrator;

public sealed class PublishQuizOrchestratorHandler(ISender sender):IRequestHandler<PublishQuizOrchestrator, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(PublishQuizOrchestrator request,CancellationToken cancellationToken)
    {
        var quizResult = await sender.Send(new GetQuizForPublishCheckQuery(request.QuizId),cancellationToken);

        if (quizResult.IsError)
            return quizResult.TopError;

        var publishCheck = PublishCheckEvaluator.Evaluate(quizResult.Value);

        if (!publishCheck.IsReady)
        {
            var errors = new List<Error>();

            if (!publishCheck.HasQuestions)
                errors.Add(PublishQuizErrors.HasNoQuestions);

            if (!publishCheck.QuestionsHaveExactlyOneCorrectOption)
                errors.Add(PublishQuizErrors.QuestionsMustHaveExactlyOneCorrectOption);

            if (!publishCheck.DurationIsValid)
                errors.Add(PublishQuizErrors.InvalidDuration);

            if (!publishCheck.PassScoreIsValid)
                errors.Add(PublishQuizErrors.InvalidPassScore);

            return Result<Updated>.Failure(errors);
        }

        return await sender.Send(new PublishQuizCommand(request.QuizId),cancellationToken);
    }
}