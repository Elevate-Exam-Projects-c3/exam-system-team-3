namespace exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;


public sealed record DiplomaDetailsResponse(
    Guid Id,
    string Title,
    string? Description,
    IReadOnlyList<DiplomaQuizResponse> Quizzes);