namespace Domain.Shared;

public class Result
{
    public Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T>
{
    public Result(bool succeeded, IEnumerable<T> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }
    public T[] Errors { get; init; }

    public static Result<T> Success()
    {
        return new Result<T>(true, Array.Empty<T>());
    }

    public static Result<T> Failure(IEnumerable<T> errors)
    {
        return new Result<T>(false, errors);
    }
}