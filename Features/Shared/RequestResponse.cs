using exam_system.Features.Shared.Results;
using exam_system.Features.Shared.Results.Abstractions;

namespace exam_system.Features.Shared;

public class RequestResponse<T> : IErrorResponse<RequestResponse<T>>
{
    public bool Success { get; init; }
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
    public IDictionary<string, string[]>? Errors { get; init; }

    public static RequestResponse<T> Ok(
        T data,
        string message = "Success",
        int statusCode = StatusCodes.Status200OK)
        => new()
        {
            Success = true,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };

    public static RequestResponse<T> Created(
        T data,
        string message = "Created successfully")
        => new()
        {
            Success = true,
            StatusCode = StatusCodes.Status201Created,
            Message = message,
            Data = data
        };

    public static RequestResponse<T> Fail(Error error)
        => new()
        {
            Success = false,
            StatusCode = MapErrorKindToStatusCode(error.Type),
            Message = error.Description
        };

    public static RequestResponse<T> Fail(
        string message,
        int statusCode = StatusCodes.Status400BadRequest,
        IDictionary<string, string[]>? errors = null)
        => new()
        {
            Success = false,
            StatusCode = statusCode,
            Message = message,
            Errors = errors
        };

    private static int MapErrorKindToStatusCode(ErrorKind type) => type switch
    {
        ErrorKind.Validation => StatusCodes.Status400BadRequest,
        ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
        ErrorKind.NotFound => StatusCodes.Status404NotFound,
        ErrorKind.Conflict => StatusCodes.Status409Conflict,
        ErrorKind.Gone => StatusCodes.Status410Gone,
        ErrorKind.TooManyRequests => StatusCodes.Status429TooManyRequests,
        _ => StatusCodes.Status500InternalServerError
    };
}