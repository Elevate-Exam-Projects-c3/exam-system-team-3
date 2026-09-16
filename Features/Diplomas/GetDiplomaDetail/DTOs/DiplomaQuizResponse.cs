namespace exam_system.Features.Diplomas.GetDiplomaDetail.DTOs;

public sealed record DiplomaQuizResponse(
    Guid Id,
    string Title,
    int DurationMinutes,
    double PassScore,
    int? MaxAttempts,
    bool CanAttempt,
    bool IsResumable);