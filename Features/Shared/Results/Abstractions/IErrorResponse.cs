namespace exam_system.Features.Shared.Results.Abstractions;

public interface IErrorResponse<TSelf> where TSelf : IErrorResponse<TSelf>
{
    static abstract TSelf Fail(string message, int statusCode = 400, IDictionary<string, string[]>? errors = null);
}