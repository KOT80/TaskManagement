using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Tasks.CreateTask;

public class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Guid> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = new TaskItem(
            request.Title,
            request.Description);

        await _taskRepository.AddAsync(
            task,
            cancellationToken);

        return task.Id;
    }
}