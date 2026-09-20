using exam_system.Features.Dashboard.AdminDashboard.DTOs;
namespace exam_system.Features.Dashboard.AdminDashboard.Queries;


public sealed record GetAdminDashboardQuery: IRequest<Result<AdminDashboardResponse>>;