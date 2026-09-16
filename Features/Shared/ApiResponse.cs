namespace exam_system.Features.Shared;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
    public IDictionary<string, string[]>? Errors { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;


    public static ApiResponse<T> Ok(
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

    public static ApiResponse<T> Created(
        T data,
        string message = "Created successfully")
        => new()
        {
            Success = true,
            StatusCode = StatusCodes.Status201Created,
            Message = message,
            Data = data
        };

    public static ApiResponse<T> Fail(
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