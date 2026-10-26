using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.ChangeTaskCompletion;

public class ChangeTaskCompletionCommandHandler
    : IRequestHandler<ChangeTaskCompletionCommand, bool>
{
    private readonly ITaskRepository _repository;

    public ChangeTaskCompletionCommandHandler(
        ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        ChangeTaskCompletionCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (task is null)
        {
            return false;
        }

        if (request.IsCompleted)
        {
            task.Complete();
        }
        else
        {
            task.Reopen();
        }

        await _repository.UpdateAsync(task, cancellationToken);

        return true;
    }
}