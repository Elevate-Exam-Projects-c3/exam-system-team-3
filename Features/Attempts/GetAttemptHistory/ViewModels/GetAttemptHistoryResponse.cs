namespace exam_system.Features.Attempts.GetAttemptHistory.ViewModels
{
    public sealed class GetAttemptHistoryResponse 
    { 
        public List<AttemptHistoryItemViewModel> Attempts { get; set; } = new(); 
    }
}
