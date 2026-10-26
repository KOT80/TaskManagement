namespace TaskManagement.Api.Contracts.Tasks;

public record UpdateTaskRequest(
    string Title,
    string? Description);