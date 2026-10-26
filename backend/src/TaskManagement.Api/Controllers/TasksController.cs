using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Contracts.Tasks;
using TaskManagement.Application.Tasks.Commands.ChangeTaskCompletion;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Tasks.Commands.UpdateTask;
using TaskManagement.Application.Tasks.CreateTask;
using TaskManagement.Application.Tasks.DTOs;
using TaskManagement.Application.Tasks.Queries.GetTaskById;
using TaskManagement.Application.Tasks.Queries.GetTasks;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/v1/tasks")]
public class TasksController : ControllerBase
{
    private readonly ISender _sender;

    public TasksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var taskId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = taskId },
            taskId);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> GetAll(
        [FromQuery] bool? isCompleted,
        CancellationToken cancellationToken)
    {
        var tasks = await _sender.Send(
            new GetTasksQuery(isCompleted),
            cancellationToken);

        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var task = await _sender.Send(
            new GetTaskByIdQuery(id),
            cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _sender.Send(
            new UpdateTaskCommand(id, request.Title, request.Description),
            cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    [HttpPatch("{id:guid}/completion")]
    public async Task<IActionResult> ChangeCompletion(
        Guid id,
        [FromBody] ChangeTaskCompletionRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _sender.Send(
            new ChangeTaskCompletionCommand(id, request.IsCompleted),
            cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _sender.Send(
            new DeleteTaskCommand(id),
            cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}