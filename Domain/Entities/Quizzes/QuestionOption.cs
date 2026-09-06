using exam_system.Domain.Common;
using exam_system.Domain.Entities.Attempts;
using System.Security.Cryptography;

namespace exam_system.Domain.Entities.Quizzes;

public class QuestionOption : BaseEntity
{
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public string OptionText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;

    public HashSet<StudentQuestionAnswer> SelectedInAnswers { get; set; } = new HashSet<StudentQuestionAnswer>();
}
