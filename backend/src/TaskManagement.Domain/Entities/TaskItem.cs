namespace TaskManagement.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

   
    private TaskItem()
    {
    }

  
    public TaskItem(string title, string? description = null)
    {
        ValidateTitle(title);

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    
    public void Update(string title, string? description)
    {
        ValidateTitle(title);

        Title = title;
        Description = description;
    }

   
    public void Complete()
    {
        IsCompleted = true;
    }

   
    public void Reopen()
    {
        IsCompleted = false;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Заголовок не может быть пустым.",
                nameof(title));
        }

        if (title.Length > 200)
        {
            throw new ArgumentException(
                "Заголовок не должен привышать 200 символов.",
                nameof(title));
        }
    }
}