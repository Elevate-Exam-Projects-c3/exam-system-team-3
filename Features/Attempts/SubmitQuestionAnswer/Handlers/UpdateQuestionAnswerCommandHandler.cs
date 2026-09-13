using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Commands;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public sealed class UpdateQuestionAnswerCommandHandler(IGenericRepository<StudentQuestionAnswer> _answerRepository): IRequestHandler<UpdateQuestionAnswerCommand,Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateQuestionAnswerCommand request,CancellationToken cancellationToken)
        {
            var answer = await _answerRepository.GetByIdAsync(request.AnswerId);

            if (answer is null || answer.IsDeleted)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "ANSWER_NOT_FOUND",
                        "Student question answer was not found."));
            }

            answer.SelectedOptionId = request.SelectedOptionId;
            answer.IsCorrect = request.IsCorrect;
            answer.AnsweredAt = DateTime.UtcNow;

            await _answerRepository.UpdateAsync(answer);

            await _answerRepository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(answer.Id);
        }
    }
}
