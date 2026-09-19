using exam_system.Features.Quizzes.AdminQuizPublishCheck.DTOs;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Services;

public static class PublishCheckEvaluator
{
    public static PublishCheckResponse Evaluate(PublishCheckData quiz)
    {
        var hasQuestions = quiz.QuestionsCount > 0;

        var questionsHaveExactlyOneCorrectOption =
            quiz.CorrectOptionsCountPerQuestion.All(correctOptionsCount =>
                correctOptionsCount == 1);

        var durationIsValid = quiz.DurationMinutes > 0;

        var passScoreIsValid = quiz.PassScore is >= 0 and <= 100;

        var isReady =
            hasQuestions &&
            questionsHaveExactlyOneCorrectOption &&
            durationIsValid &&
            passScoreIsValid;

        return new PublishCheckResponse(
            isReady,
            hasQuestions,
            questionsHaveExactlyOneCorrectOption,
            durationIsValid,
            passScoreIsValid);
    }
}