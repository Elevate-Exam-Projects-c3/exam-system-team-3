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
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .Select(error => error.ErrorMessage)
                        .ToArray());

            var validationResponse = ApiResponse<object>.Fail(
                message: "Validation failed.",
                statusCode: StatusCodes.Status400BadRequest,
                errors: errors);

            httpContext.Response.StatusCode = validationResponse.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(
                validationResponse,
                cancellationToken);

            return true;
        }

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