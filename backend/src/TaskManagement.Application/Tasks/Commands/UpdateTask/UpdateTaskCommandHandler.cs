using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _repository;

    public UpdateTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (task is null)
        {
            return false;
        }

        task.Update(request.Title, request.Description);

        await _repository.UpdateAsync(task, cancellationToken);

        return true;
    }
}