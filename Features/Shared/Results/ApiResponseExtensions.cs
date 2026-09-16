using exam_system.Features.Shared.Results;

namespace exam_system.Features.Shared;

public static class ApiResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return ApiResponse<T>.Ok(result.Value);
        }

        var error = result.TopError;

        return ApiResponse<T>.Fail(
            message: error.Description,
            statusCode: MapErrorKindToStatusCode(error.Type));
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
        _ => StatusCodes.Status500InternalServerError
    };
}