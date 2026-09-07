namespace exam_system.Features.Shared.Results;

public static class Result
{
    public static Success Success => default;

    public static Created Created => default;

    public static Deleted Deleted => default;

    public static Updated Updated => default;
}

public sealed class Result<TValue> : IResult<TValue>
{
    private readonly TValue? _value;
    private readonly IReadOnlyList<Error> _errors;

    private Result(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);

        _value = value;
        _errors = [];
        IsSuccess = true;
    }

    private Result(Error error)
    {
        _errors = [error];
        IsSuccess = false;
    }

    private Result(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var errorList = errors.ToList();

        if (errorList.Count == 0)
        {
            throw new ArgumentException(
                "At least one error is required.",
                nameof(errors));
        }

        _errors = errorList;
        IsSuccess = false;
    }

    public bool IsSuccess { get; }

    public bool IsError => !IsSuccess;

    public IReadOnlyList<Error> Errors => _errors;

    public TValue Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "Cannot access the value of a failed result.");

    public Error TopError =>
        IsError
            ? _errors[0]
            : throw new InvalidOperationException(
                "Cannot access errors from a successful result.");

    public TNextValue Match<TNextValue>(
        Func<TValue, TNextValue> onValue,
        Func<IReadOnlyList<Error>, TNextValue> onError)
    {
        ArgumentNullException.ThrowIfNull(onValue);
        ArgumentNullException.ThrowIfNull(onError);

        return IsSuccess
            ? onValue(Value)
            : onError(Errors);
    }

    public static Result<TValue> Success(TValue value)
        => new(value);

    public static Result<TValue> Failure(Error error)
        => new(error);

    public static Result<TValue> Failure(IEnumerable<Error> errors)
        => new(errors);

    public static implicit operator Result<TValue>(TValue value)
        => new(value);

    public static implicit operator Result<TValue>(Error error)
        => new(error);

    public static implicit operator Result<TValue>(List<Error> errors)
        => new(errors);
}

public readonly record struct Success;

public readonly record struct Created;

public readonly record struct Deleted;

public readonly record struct Updated;