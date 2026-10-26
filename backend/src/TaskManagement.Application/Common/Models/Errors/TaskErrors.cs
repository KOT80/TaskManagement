namespace TaskManagement.Application.Common.Models.Errors;

public static class TaskErrors
{
    public static Error NotFound(Guid id) =>
        new(
            "Task.NotFound",
            $"Задача с идентификатором '{id}' не найдена.",
            ErrorType.NotFound);

    public static Error TitleRequired =>
        new(
            "Task.Title.Required",
            "Название задачи обязательно.",
            ErrorType.Validation);

    public static Error TitleTooLong =>
        new(
            "Task.Title.TooLong",
            "Название задачи не должно превышать 200 символов.",
            ErrorType.Validation);
}