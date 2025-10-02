using Microsoft.AspNetCore.Mvc;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Tags("People")]
[Route("api/v{version:apiVersion}/people")]
public class PeopleControllerV2 : BaseApiController
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Message = "Hii",
            Version = "v2"
        });
    }
}