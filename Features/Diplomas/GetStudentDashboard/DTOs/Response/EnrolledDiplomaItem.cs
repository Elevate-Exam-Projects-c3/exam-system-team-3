namespace exam_system.Features.Diplomas.GetStudentDashboard.DTOs.Response;

public record EnrolledDiplomaItem(
    Guid DiplomaId,
    string Title,
    DateTime EnrolledAt
);