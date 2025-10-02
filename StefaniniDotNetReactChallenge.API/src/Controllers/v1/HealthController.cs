using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("1.0"), ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/health")]
[Tags("HealthCheck")]
public class HealthController : BaseApiController
{
    private readonly HealthCheckService _healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        return report.Status == HealthStatus.Healthy ? Ok("Healthy") : StatusCode(503, "Unhealthy");
    }
}