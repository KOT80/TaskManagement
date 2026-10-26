using MediatR;
using TaskManagement.Application.Common.Models;
using TaskManagement.Application.Tasks.DTOs;

namespace TaskManagement.Application.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid Id) : IRequest<Result<TaskDto>?>;