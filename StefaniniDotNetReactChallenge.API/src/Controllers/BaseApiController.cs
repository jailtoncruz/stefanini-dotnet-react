using Microsoft.AspNetCore.Mvc;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
}