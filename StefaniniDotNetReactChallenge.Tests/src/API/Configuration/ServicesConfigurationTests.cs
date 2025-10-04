using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StefaniniDotNetReactChallenge.API.Configurations;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Application.Services;
using StefaniniDotNetReactChallenge.Domain.Interfaces;
using Xunit;

namespace StefaniniDotNetReactChallenge.Tests.API.Configuration
{
    public class ServicesConfigurationTests
    {
        [Fact]
        public void AddServicesConfiguration_Should_Register_IPersonService()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            services.AddSingleton(new Mock<IPersonRepository>().Object);

            // Act
            services.AddServicesConfiguration(configuration);
            var provider = services.BuildServiceProvider();

            // Assert
            provider.GetService<IPersonService>().Should().NotBeNull();
            provider.GetService<IPersonService>().Should().BeOfType<PersonService>();
        }
    }
}