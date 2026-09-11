using exam_system.Common.Enums;
using exam_system.Features.Questions.AdminDeleteQuestion.Orchestrators;
using exam_system.Features.Questions.AdminDeleteQuestion.Queries;
using exam_system.Features.Shared.Results;

namespace exam_system.Features.Questions.AdminDeleteQuestion.Handlers
{
    public class DeleteQuestionOrchestratorHandler(IMediator _mediator): IRequestHandler<DeleteQuestionOrchestrator,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(DeleteQuestionOrchestrator request,CancellationToken cancellationToken)
        {
            var question = await _mediator.Send(new GetQuestionWithQuizQuery(request.QuestionId),cancellationToken);

            if (question is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "Question not found."));
            }

            if (question.QuizId != request.QuizId)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "QUESTION_NOT_FOUND",
                        "Question not found in this quiz."));
            }

            // 3. Published quiz guard
            if (question.Quiz.Status == QuizStatus.Published)
            {
                return Result<Guid>.Failure(
                    Error.Conflict(
                        "QUIZ_IS_PUBLISHED",
                        "Question cannot be deleted while the quiz is published. Unpublish the quiz first."));
            }

            // 4. Soft delete
            return await _mediator.Send(
                new DeleteQuestionCommand(question),
                cancellationToken);
        }
    }
}
