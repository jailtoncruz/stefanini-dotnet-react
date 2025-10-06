using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace StefaniniDotNetReactChallenge.API.Controllers;

public record LoginRequest(string Username);

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[Tags("Auth")]
public class AuthControllerV1 : BaseApiController
{
    private readonly string _jwt_key;
    private readonly string _jwt_issuer;
    private readonly HashSet<string> _authorizedUsernames;
    public AuthControllerV1(IConfiguration config)
    {

        _jwt_key = config["Jwt:Key"]!;
        _jwt_issuer = config["Jwt:Issuer"]!;

        _authorizedUsernames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Stefanini",
            "Jailton",
            "Andressa"
        };
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!_authorizedUsernames.Contains(request.Username))
            return BadRequest(new
            {
                Error = "Not authorized"
            });

        var token = GenerateJwtToken(request.Username);
        return Ok(new { token });
    }

    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Guest")
        };

        var token = new JwtSecurityToken(
            issuer: _jwt_issuer,
            audience: _jwt_issuer,
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}