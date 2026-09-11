namespace exam_system.Features.Shared.Results;

public static class ResultExtensions
{
    public static RequestResponse<T> ToRequestResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return RequestResponse<T>.Ok(result.Value);
        }

        var topError = result.TopError;

        var statusCode = topError.Type switch
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

        var errors = result.Errors
                           .GroupBy(error => error.Code)
                           .ToDictionary(
                            group => group.Key,
                            group => group
                           .Select(error => error.Description)
                           .ToArray());

        return RequestResponse<T>.Fail(topError.Description, statusCode, errors);
    }
}