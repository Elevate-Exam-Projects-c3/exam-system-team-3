using exam_system.Domain.Entities.Attempts;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Commands;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public sealed class SubmitQuestionAnswerCommandHandler(IGenericRepository<StudentQuestionAnswer> _answerRepository): IRequestHandler<SubmitQuestionAnswerCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(SubmitQuestionAnswerCommand request,CancellationToken cancellationToken)
        {
            var answer = new StudentQuestionAnswer
            {
                AttemptId = request.AttemptId,
                QuestionId = request.QuestionId,
                SelectedOptionId = request.SelectedOptionId,
                IsCorrect = request.IsCorrect,
                AnsweredAt = DateTime.UtcNow
            };

            await _answerRepository.AddAsync(answer);

            await _answerRepository.SaveChangesAsync(
                cancellationToken);

            return Result<Guid>.Success(answer.Id);
        }
    }
}
