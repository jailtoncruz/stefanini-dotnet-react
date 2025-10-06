using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StefaniniDotNetReactChallenge.API.Controllers;

namespace StefaniniDotNetReactChallenge.Tests.Controllers
{
    public record TokenResponse(string token);

    public class AuthControllerV1Tests
    {
        private readonly IConfiguration _config;
        private readonly AuthControllerV1 _controller;

        public AuthControllerV1Tests()
        {
            // Arrange: build a fake configuration for JWT
            var inMemorySettings = new Dictionary<string, string?>
            {
                {"Jwt:Key", "test_secret_key_123456789_123456789_123456789_123456789"},
                {"Jwt:Issuer", "stefanini-challenge.test"}
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _controller = new AuthControllerV1(_config);
        }

        [Fact]
        public void Login_ShouldReturn_ValidJwtToken()
        {
            // Arrange
            var request = new LoginRequest("Jailton");

            // Act
            var result = _controller.Login(request) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);

            var tokenProperty = result!.Value!.GetType().GetProperty("token");
            string token = (string)tokenProperty!.GetValue(result.Value)!;
            token.Should().NotBeNullOrEmpty();

            // Validate the token
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            jwt.Should().NotBeNull();
            jwt.Issuer.Should().Be("stefanini-challenge.test");

            // Check claims
            var nameClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            nameClaim.Should().NotBeNull();
            nameClaim!.Value.Should().Be("Jailton");

            roleClaim.Should().NotBeNull();
            roleClaim!.Value.Should().Be("Guest");
        }

        [Fact]
        public void GeneratedToken_ShouldBeValid_UsingSameKey()
        {
            // Arrange
            var request = new LoginRequest("Jailton");
            var result = _controller.Login(request) as OkObjectResult;

            // Extract token from anonymous object via reflection
            var tokenProperty = result!.Value!.GetType().GetProperty("token");
            string token = (string)tokenProperty!.GetValue(result.Value)!;


            // Act
            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!))
            };

            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, validationParams, out var validatedToken);

            // Assert
            validatedToken.Should().NotBeNull();
            validatedToken.Should().BeOfType<JwtSecurityToken>();
        }
    }
}
