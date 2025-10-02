using Microsoft.AspNetCore.Mvc;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/people")]
[Tags("People")]
public class PeopleControllerV1 : BaseApiController
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Message = "Hii"
        });
    }
}