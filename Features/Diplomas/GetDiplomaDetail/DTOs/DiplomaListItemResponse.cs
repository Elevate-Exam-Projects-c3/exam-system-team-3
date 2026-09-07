namespace exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;


public sealed record DiplomaListItemResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int CompletedQuizzes { get; init; }
    public int TotalQuizzes { get; init; }
}