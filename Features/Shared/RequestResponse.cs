namespace exam_system.Features.Shared;

public class RequestResponse<T>
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
}
