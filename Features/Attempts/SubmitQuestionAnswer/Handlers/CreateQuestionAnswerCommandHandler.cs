using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Commands;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class CreateQuestionAnswerCommandHandler(IGenericRepository<StudentQuestionAnswer> _answerRepository): IRequestHandler<CreateQuestionAnswerCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateQuestionAnswerCommand request,CancellationToken cancellationToken)
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

            await _answerRepository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(answer.Id);
        }
    }
}
