namespace exam_system.Features.Shared;

public static class ApiResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this RequestResponse<T> result)
    {
        return new ApiResponse<T>
        {
            Success = result.Success,
            StatusCode = result.StatusCode,
            Message = result.Message,
            Data = result.Data,
            Errors = result.Errors
        };
    }
}