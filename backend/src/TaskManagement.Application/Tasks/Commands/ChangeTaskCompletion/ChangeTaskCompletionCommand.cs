using MediatR;

namespace TaskManagement.Application.Tasks.Commands.ChangeTaskCompletion;

public record ChangeTaskCompletionCommand(
    Guid Id,
    bool IsCompleted
) : IRequest<bool>;