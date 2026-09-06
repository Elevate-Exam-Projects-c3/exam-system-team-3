using exam_system.Domain.Common;
using exam_system.Domain.Entities.Attempts;

namespace exam_system.Domain.Entities.Quizzes;

public class Question : BaseEntity
{
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public string Text { get; set; } = string.Empty;
    public string? Explanation { get; set; }
    public int OrderIndex { get; set; }

    // Navigations
    public HashSet<QuestionOption> Options { get; set; } = new HashSet<QuestionOption>();
    public HashSet<StudentQuestionAnswer> Answers { get; set; } = new HashSet<StudentQuestionAnswer>();
}
