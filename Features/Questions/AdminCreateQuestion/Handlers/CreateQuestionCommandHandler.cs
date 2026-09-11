using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Questions.AdminCreateQuestion.Commands;
using exam_system.Features.Shared.Results;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Questions.AdminCreateQuestion.Handlers
{
    public class CreateQuestionCommandHandler(IGenericRepository<Question> _questionRepo) : IRequestHandler<CreateQuestionCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateQuestionCommand request,CancellationToken cancellationToken)
        {
            var data = request.Request;

            var question = new Question
            {
                QuizId = request.QuizId,
                Text = data.Text.Trim(),
                Explanation = data.Explanation?.Trim(),
                OrderIndex = data.OrderIndex
            };

            foreach (var option in data.Options)
            {
                question.Options.Add(new QuestionOption
                    {
                        OptionText = option.OptionText.Trim(),
                        IsCorrect = option.IsCorrect
                    });
            }

            await _questionRepo.AddAsync(question);

            await _questionRepo.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(question.Id);
        }
    }
}
