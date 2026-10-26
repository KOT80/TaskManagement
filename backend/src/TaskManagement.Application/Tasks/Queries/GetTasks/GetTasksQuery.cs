using MediatR;
using TaskManagement.Application.Tasks.DTOs;

namespace TaskManagement.Application.Tasks.Queries.GetTasks;

public record GetTasksQuery(bool? IsCompleted)
    : IRequest<IReadOnlyList<TaskDto>>;