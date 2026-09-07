namespace exam_system.Features.Shared.Results;

public enum ErrorKind
{
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden,
    Gone,
    TooManyRequests,
    AlreadyExists,
}