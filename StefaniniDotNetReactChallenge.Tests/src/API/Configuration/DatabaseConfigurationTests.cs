using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StefaniniDotNetReactChallenge.API.Configurations;
using StefaniniDotNetReactChallenge.Infrastructure.Data;
using Xunit;

namespace StefaniniDotNetReactChallenge.Tests.API.Configuration
{
    public class DatabaseConfigurationTests
    {
        [Fact]
        public void AddDatabaseConfiguration_Should_Register_AppDbContext()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDatabaseConfiguration(configuration);
            var provider = services.BuildServiceProvider();

            // Assert
            provider.GetService<AppDbContext>().Should().NotBeNull();
        }
    }
}
