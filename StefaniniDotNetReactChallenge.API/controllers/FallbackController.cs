using Microsoft.AspNetCore.Mvc;

namespace StefaniniDotNetReactChallenge.API.Controllers;

public class FallbackController : Controller
{
    public IActionResult Index()
    {
        Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"));
        return PhysicalFile(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
            "text/html"
        );
    }
}