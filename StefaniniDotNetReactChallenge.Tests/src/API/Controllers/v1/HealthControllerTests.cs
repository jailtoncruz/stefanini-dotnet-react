using StefaniniDotNetReactChallenge.API.Controllers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace StefaniniDotNetReactChallenge.Tests.API.Controllers;

public class TestHealthCheckService : HealthCheckService
{
    private readonly HealthReport _healthReportToReturn;

    public TestHealthCheckService(HealthReport healthReportToReturn)
    {
        _healthReportToReturn = healthReportToReturn;
    }

    public override Task<HealthReport> CheckHealthAsync(
        Func<HealthCheckRegistration, bool>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_healthReportToReturn);
    }
}

public static class HealthReportTestHelper
{
    public static HealthReport CreateHealthReport(HealthStatus overallStatus)
    {
        var reportEntries = new Dictionary<string, HealthReportEntry>
        {
            ["TestService"] = new HealthReportEntry(
                overallStatus,
                $"Service is {overallStatus}",
                TimeSpan.Zero,
                null,
                null)
        };

        return new HealthReport(reportEntries, overallStatus, TimeSpan.Zero);
    }

    public static HealthReport CreateHealthReport(HealthStatus overallStatus, params (string name, HealthStatus status)[] entries)
    {
        var reportEntries = entries.ToDictionary(
            e => e.name,
            e => new HealthReportEntry(e.status, $"Description for {e.name}", TimeSpan.Zero, null, null)
        );

        return new HealthReport(reportEntries, overallStatus, TimeSpan.Zero);
    }
}

public class HealthControllerTests
{
    // REMOVED: No need for Mock objects when using concrete test class
    // private readonly Mock<TestHealthCheckService> _healthCheckServiceMock;

    // Also removed the controller field since we're creating it in each test

    [Fact]
    public async Task Get_WhenHealthy_ReturnsOkWithHealthyMessage()
    {
        // Arrange
        var healthReport = HealthReportTestHelper.CreateHealthReport(HealthStatus.Healthy);
        var testHealthService = new TestHealthCheckService(healthReport);
        var controller = new HealthController(testHealthService);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be("Healthy");
    }

    [Fact]
    public async Task Get_WhenUnhealthy_Returns503WithUnhealthyMessage()
    {
        // Arrange
        var healthReport = HealthReportTestHelper.CreateHealthReport(HealthStatus.Unhealthy);
        var testHealthService = new TestHealthCheckService(healthReport);
        var controller = new HealthController(testHealthService);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().BeOfType<ObjectResult>();
        var objectResult = result as ObjectResult;
        objectResult!.StatusCode.Should().Be(503);
        objectResult.Value.Should().Be("Unhealthy");
    }

    [Fact]
    public async Task Get_WhenDegraded_Returns503WithUnhealthyMessage()
    {
        // Arrange
        var healthReport = HealthReportTestHelper.CreateHealthReport(HealthStatus.Degraded);
        var testHealthService = new TestHealthCheckService(healthReport);
        var controller = new HealthController(testHealthService);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().BeOfType<ObjectResult>();
        var objectResult = result as ObjectResult;
        objectResult!.StatusCode.Should().Be(503);
        objectResult.Value.Should().Be("Unhealthy");
    }

    [Fact]
    public async Task Get_WithMultipleHealthEntries_ReturnsBasedOnOverallStatus()
    {
        // Arrange - Mixed status but overall Healthy
        var healthReport = HealthReportTestHelper.CreateHealthReport(
            HealthStatus.Healthy,
            ("Database", HealthStatus.Healthy),
            ("ExternalService", HealthStatus.Degraded)
        );

        var testHealthService = new TestHealthCheckService(healthReport);
        var controller = new HealthController(testHealthService);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be("Healthy");
    }
}