using Microsoft.AspNetCore.Mvc;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("2.0")]
[Tags("Auth")]
public class AuthControllerV2(IConfiguration config) : AuthControllerV1(config)
{
}