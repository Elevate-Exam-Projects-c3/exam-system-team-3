namespace exam_system.Features.Diplomas.GetDiplomas.DTOs;

public sealed record DiplomaListItemResponse(
    Guid Id,
    string Title,
    string? Description,
    int CompletedQuizzes,
    int TotalQuizzes);