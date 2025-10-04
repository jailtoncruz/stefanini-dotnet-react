using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StefaniniDotNetReactChallenge.API.Configurations;

namespace StefaniniDotNetReactChallenge.Tests.Configurations;

public class AuthenticationConfigurationTests
{
    [Fact]
    public void AddAuthenticationConfiguration_ShouldConfigureJwtOptionsProperly()
    {
        // Arrange
        var settings = new Dictionary<string, string?>
        {
            {"Jwt:Key", "test_secret_key_123"},
            {"Jwt:Issuer", "stefanini.test"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        var services = new ServiceCollection();

        // Act
        services.AddAuthenticationConfiguration(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var authOptions = provider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>()
            .Get(JwtBearerDefaults.AuthenticationScheme);

        authOptions.TokenValidationParameters.ValidIssuer.Should().Be("stefanini.test");
        authOptions.TokenValidationParameters.IssuerSigningKey
            .Should().BeOfType<SymmetricSecurityKey>();

        var key = (authOptions.TokenValidationParameters.IssuerSigningKey as SymmetricSecurityKey)!;
        Encoding.UTF8.GetString(key.Key).Should().Be("test_secret_key_123");
    }
}
