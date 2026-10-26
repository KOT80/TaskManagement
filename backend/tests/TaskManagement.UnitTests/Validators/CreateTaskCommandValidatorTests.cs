using FluentValidation.TestHelper;
using TaskManagement.Application.Tasks.Commands.CreateTask;


namespace TaskManagement.UnitTests.Validators;

public class CreateTaskCommandValidatorTests
{
    private readonly CreateTaskCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidCommand_HasNoErrors()
    {
        const string title = "TEstst";
        const string description = "Testing Task";
        
        var command = new CreateTaskCommand(
            title,
            description);

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithEmptyTitle_HasValidationError(string title)
    {
        var command = new CreateTaskCommand(title, string.Empty);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WithTitleLongerThan200Characters_HasValidationError()
    {
        var command = new CreateTaskCommand(
            new string('a', 201),
            string.Empty);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WithTitleOf200Characters_HasNoTitleError()
    {
        var command = new CreateTaskCommand(
            new string('a', 200),
            string.Empty);

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_WithDescriptionLongerThan2000Characters_HasValidationError()
    {
        var command = new CreateTaskCommand(
            "Title is good",
            new string('a', 2001));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}