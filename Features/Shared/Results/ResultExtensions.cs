using exam_system.Features.Shared.Results;

namespace exam_system.Features.Shared;

public static class ResultExtensions
{
    public static RequestResponse<T> ToRequestResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return RequestResponse<T>.Ok(result.Value);
        }

        var error = result.TopError;

        var statusCode = error.Type switch
        {
            ErrorKind.Validation => StatusCodes.Status400BadRequest,
            ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
            ErrorKind.NotFound => StatusCodes.Status404NotFound,
            ErrorKind.Conflict => StatusCodes.Status409Conflict,
            ErrorKind.Gone => StatusCodes.Status410Gone,
            ErrorKind.TooManyRequests => StatusCodes.Status429TooManyRequests,
            ErrorKind.Unexpected => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };

        return RequestResponse<T>.Fail(
            error.Description,
            statusCode);
    }
}