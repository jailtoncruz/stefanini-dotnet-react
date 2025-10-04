using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StefaniniDotNetReactChallenge.API.Configurations;
using StefaniniDotNetReactChallenge.Domain.Interfaces;
using StefaniniDotNetReactChallenge.Infrastructure.Data;
using StefaniniDotNetReactChallenge.Infrastructure.Repositories;
using Xunit;

namespace StefaniniDotNetReactChallenge.Tests.API.Configuration
{
    public class RepositoriesConfigurationTests
    {
        [Fact]
        public void AddRepositoriesConfiguration_Should_Register_IPersonRepository()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            // Act
            services.AddRepositoriesConfiguration(configuration);
            var provider = services.BuildServiceProvider();

            // Assert
            provider.GetService<IPersonRepository>().Should().NotBeNull();
            provider.GetService<IPersonRepository>().Should().BeOfType<PersonRepository>();
        }
    }
}