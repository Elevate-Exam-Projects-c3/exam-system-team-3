namespace exam_system.Features.Shared.Results;

public readonly record struct Error
{
    private Error(
        string code,
        string description,
        ErrorKind type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }

    public string Description { get; }

    public ErrorKind Type { get; }

    public static Error Failure(
        string code = nameof(Failure),
        string description = "General failure.")
        => new(code, description, ErrorKind.Failure);

    public static Error Unexpected(
        string code = nameof(Unexpected),
        string description = "Unexpected error.")
        => new(code, description, ErrorKind.Unexpected);

    public static Error Validation(
        string code = nameof(Validation),
        string description = "Validation error.")
        => new(code, description, ErrorKind.Validation);

    public static Error Conflict(
        string code = nameof(Conflict),
        string description = "Conflict error.")
        => new(code, description, ErrorKind.Conflict);

    public static Error NotFound(
        string code = nameof(NotFound),
        string description = "Resource was not found.")
        => new(code, description, ErrorKind.NotFound);

    public static Error Unauthorized(
        string code = nameof(Unauthorized),
        string description = "Authentication is required.")
        => new(code, description, ErrorKind.Unauthorized);

    public static Error Forbidden(
        string code = nameof(Forbidden),
        string description = "You are not authorized to perform this action.")
        => new(code, description, ErrorKind.Forbidden);

    public static Error Gone(
        string code = nameof(Gone),
        string description = "The requested resource is no longer available.")
        => new(code, description, ErrorKind.Gone);

    public static Error TooManyRequests(
        string code = nameof(TooManyRequests),
        string description = "Too many requests.")
        => new(code, description, ErrorKind.TooManyRequests);

    public static Error Create(
        ErrorKind type,
        string code,
        string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new(code, description, type);
    }
}