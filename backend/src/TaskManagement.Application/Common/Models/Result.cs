using TaskManagement.Application.Common.Models.Errors;

namespace TaskManagement.Application.Common.Models;

public sealed record Result<T>(
    bool IsSuccess,
    T? Value,
    Error? Error)
{
    public static Result<T> Success(T value) =>
        new(true, value, null);

    public static Result<T> Failure(Error error) =>
        new(false, default, error);
}