using FluentValidation.TestHelper;
using TaskManagement.Application.Tasks.Commands.UpdateTask;

namespace TaskManagement.UnitTests.Validators;

public class UpdateTaskCommandValidatorTests
{
    private readonly UpdateTaskCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidCommand_HasNoErrors()
    {
        const string title = "TEstst";
        const string description = "Testing Task";
        
        // Arrange
        var command = new UpdateTaskCommand(
            Guid.NewGuid(),
            title +"Updated",
            description+"Updated");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyId_HasValidationError()
    {
        // Arrange
        var command = new UpdateTaskCommand(
            Guid.Empty,
            "Valid title",
            null);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithEmptyTitle_HasValidationError(string title)
    {
        // Arrange
        var command = new UpdateTaskCommand(
            Guid.NewGuid(),
            title,
            null);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WithTitleLongerThan200Characters_HasValidationError()
    {
        // Arrange
        var command = new UpdateTaskCommand(
            Guid.NewGuid(),
            new string('a', 201),
            null);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WithTitleOf200Characters_HasNoTitleError()
    {
        // Arrange
        var command = new UpdateTaskCommand(
            Guid.NewGuid(),
            new string('a', 200),
            null);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }
}
