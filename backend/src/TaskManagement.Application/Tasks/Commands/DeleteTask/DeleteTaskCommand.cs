using MediatR;

namespace TaskManagement.Application.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;