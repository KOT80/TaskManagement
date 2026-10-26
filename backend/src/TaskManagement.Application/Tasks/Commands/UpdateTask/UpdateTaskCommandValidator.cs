using FluentValidation;

namespace TaskManagement.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandValidator
    : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Идентификатор задачи обязателен.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Название задачи обязательно.")
            .MaximumLength(200)
            .WithMessage("Название задачи не должно превышать 200 символов.");
    }
}