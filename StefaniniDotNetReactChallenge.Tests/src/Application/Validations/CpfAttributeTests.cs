using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using StefaniniDotNetReactChallenge.Application.Validations;

namespace StefaniniDotNetReactChallenge.Tests.Application.Validations;

public class CpfAttributeIntegrationTests
{
    private class TestModel
    {
        [Cpf]
        public string? Cpf { get; set; }
    }

    [Fact]
    public void Validate_WithValidCpf_ReturnsSuccess()
    {
        // Arrange
        var model = new TestModel { Cpf = "52998224725" };
        var validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeTrue();
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WithInvalidCpf_ReturnsValidationError()
    {
        // Arrange
        var model = new TestModel { Cpf = "12345678900" };
        var validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().HaveCount(1);
        validationResults[0].ErrorMessage.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Validate_WithNullCpf_ReturnsValidationError()
    {
        // Arrange
        var model = new TestModel { Cpf = null };
        var validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().HaveCount(1);
    }
}