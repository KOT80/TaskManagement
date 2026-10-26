using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITaskRepository _repository;

    public DeleteTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Handle(
        DeleteTaskCommand request,
        CancellationToken cancellationToken)
    {
        return _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}