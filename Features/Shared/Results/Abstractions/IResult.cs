namespace exam_system.Features.Shared.Results;

public interface IResult
{
    bool IsSuccess { get; }

    IReadOnlyList<Error> Errors { get; }
}

public interface IResult<out TValue> : IResult
{
    TValue Value { get; }
}