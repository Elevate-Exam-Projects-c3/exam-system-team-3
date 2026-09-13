using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.StartAttempt.DTOs;
using exam_system.Features.Attempts.StartAttempt.Queries;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class GetQuizForAttemptQueryHandler(IGenericRepository<Quiz> _quizRepository) : IRequestHandler<GetQuizForAttemptQuery, QuizForAttemptDto?>
    {        
        public async Task<QuizForAttemptDto?> Handle(GetQuizForAttemptQuery request,CancellationToken cancellationToken)
        {
            return await _quizRepository
                .Get(x => x.Id == request.QuizId && !x.IsDeleted)
                .AsNoTracking()
                .Select(x => new QuizForAttemptDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    DurationMinutes = x.DurationMinutes,

                    Questions = x.Questions
                        .Where(q => !q.IsDeleted)
                        .OrderBy(q => q.OrderIndex)
                        .Select(q => new QuestionForAttemptDto
                        {
                            Id = q.Id,
                                                      
                            QuestionText = q.Text,

                            Options = q.Options
                                .Where(o => !o.IsDeleted)
                                .Select(o => new OptionForAttemptDto
                                {
                                    Id = o.Id,
                                    OptionText = o.OptionText
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
