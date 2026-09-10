using exam_system.Features.Shared;
using Microsoft.AspNetCore.Diagnostics;

namespace exam_system.Features.Shared.Results;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Unhandled exception occurred. TraceId: {TraceId}",
            httpContext.TraceIdentifier);

        var response = ApiResponse<object>.Fail(
            message: "An unexpected error occurred.",
            statusCode: StatusCodes.Status500InternalServerError);

        httpContext.Response.StatusCode = response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(
            response,
            cancellationToken);

        return true;
    }
}