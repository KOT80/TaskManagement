using FluentValidation;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandValidator
    : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Название задачи обязательно.")
            .MaximumLength(200)
            .WithMessage("Название задачи не должно превышать 200 символов.");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .WithMessage("Описание не должно превышать 2000 символов.")
            .When(x => true);
    }
}