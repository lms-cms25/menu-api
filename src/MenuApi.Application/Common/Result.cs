namespace MenuApi.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string Error { get; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    protected Result(T value, bool success, string error)
    {
        Value = value;
        IsSuccess = success;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value, true, string.Empty);
    public static Result<T> Failure(string error) => new(default!, false, error);
}
