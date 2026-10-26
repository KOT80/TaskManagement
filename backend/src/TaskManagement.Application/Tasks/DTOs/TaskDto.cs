namespace TaskManagement.Application.Tasks.DTOs;

public record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt);