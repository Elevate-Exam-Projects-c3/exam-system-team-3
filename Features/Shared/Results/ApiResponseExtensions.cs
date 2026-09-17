

namespace exam_system.Features.Shared;

public static class ApiResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result,
        int successStatusCode = StatusCodes.Status200OK)
    {
        if (result.IsSuccess)
        {

            return ApiResponse<T>.Ok(
                result.Value,
                statusCode: successStatusCode);
        }

        var topError = result.TopError;

        var errors = result.Errors
            .GroupBy(error => error.Code)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(error => error.Description)
                    .ToArray());

        return ApiResponse<T>.Fail(
            message: topError.Description,
            statusCode: MapErrorKindToStatusCode(topError.Type),
            errors: errors);
    }

    private static int MapErrorKindToStatusCode(ErrorKind errorKind) => errorKind switch
    {
        ErrorKind.Validation => StatusCodes.Status400BadRequest,
        ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
        ErrorKind.NotFound => StatusCodes.Status404NotFound,
        ErrorKind.Conflict => StatusCodes.Status409Conflict,
        ErrorKind.Gone => StatusCodes.Status410Gone,
        ErrorKind.TooManyRequests => StatusCodes.Status429TooManyRequests,
        ErrorKind.Failure => StatusCodes.Status500InternalServerError,
        ErrorKind.Unexpected => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status500InternalServerError
    };
}