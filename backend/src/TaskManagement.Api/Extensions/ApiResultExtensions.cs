using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Common.Models;
using TaskManagement.Application.Common.Models.Errors;

namespace TaskManagement.Api.Extensions;

public static class ApiResultExtensions
{
    public static IActionResult ToActionResult<T>(
        this ControllerBase controller,
        Result<T> result)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }

        return controller.ToErrorResult(result.Error!);
    }

    public static IActionResult ToErrorResult(
        this ControllerBase controller,
        Error error)
    {
        var (status, title) = error.Type switch
        {
            ErrorType.Validation =>
                (StatusCodes.Status400BadRequest, "Ошибка валидации"),

            ErrorType.NotFound =>
                (StatusCodes.Status404NotFound, "Ресурс не найден"),

            ErrorType.Conflict =>
                (StatusCodes.Status409Conflict, "Конфликт"),

            _ =>
                (StatusCodes.Status500InternalServerError,
                    "Внутренняя ошибка сервера")
        };

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = error.Message,
            Instance = controller.HttpContext.Request.Path
        };

        problem.Extensions["code"] = error.Code;
        problem.Extensions["traceId"] =
            controller.HttpContext.TraceIdentifier;

        return controller.StatusCode(status, problem);
    }
}