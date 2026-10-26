
using TaskManagement.Domain.Entities;

namespace TaskManagement.UnitTests.Domain;

public class TaskItemTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesIncompleteTask()
    {
        // Arrange
        const string title = "TEstst";
        const string description = "Testing Task";

        // Act
        var task = new TaskItem(title, description);

        // Assert
        Assert.NotEqual(Guid.Empty, task.Id);
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.False(task.IsCompleted);
        Assert.Equal(DateTimeKind.Utc, task.CreatedAt.Kind);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Constructor_WithEmptyTitle_ThrowsArgumentException(string title)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => new TaskItem(title));

        // Assert
        Assert.Equal("title", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithTitleLongerThan200Characters_ThrowsArgumentException()
    {
        // Arrange
        var title = new string('a', 201);

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => new TaskItem(title));

        // Assert
        Assert.Equal("title", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithTitleOf200Characters_Succeeds()
    {
        // Arrange
        var title = new string('a', 200);

        // Act
        var task = new TaskItem(title);

        // Assert
        Assert.Equal(title, task.Title);
    }

    [Fact]
    public void Update_WithValidData_ChangesTitleAndDescription()
    {
        // Arrange
        var task = new TaskItem("Old title", "Old description");

        // Act
        task.Update("New title", "New description");

        // Assert
        Assert.Equal("New title", task.Title);
        Assert.Equal("New description", task.Description);
    }

    [Fact]
    public void Complete_SetsIsCompletedToTrue()
    {
        // Arrange
        var task = new TaskItem("Test task");

        // Act
        task.Complete();

        // Assert
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public void Reopen_SetsIsCompletedToFalse()
    {
        // Arrange
        var task = new TaskItem("Test task");
        task.Complete();

        // Act
        task.Reopen();

        // Assert
        Assert.False(task.IsCompleted);
    }
}
