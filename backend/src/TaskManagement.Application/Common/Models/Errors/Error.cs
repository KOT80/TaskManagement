namespace TaskManagement.Application.Common.Models.Errors;

public sealed record Error(
    string Code,
    string Message,
    ErrorType Type);