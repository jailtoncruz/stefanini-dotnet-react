using FluentAssertions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StefaniniDotNetReactChallenge.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace StefaniniDotNetReactChallenge.Tests.API.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddApiVersioningConfigured_Should_Register_ApiVersioning_Services()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddControllers();

            // Act
            services.AddApiVersioningConfigured();
            var provider = services.BuildServiceProvider();

            // Assert
            provider.GetService<IApiVersionReader>().Should().NotBeNull();
            provider.GetService<IConfigureOptions<ApiVersioningOptions>>().Should().NotBeNull();
        }

        [Fact]
        public void AddSwaggerConfigured_Should_Register_Swagger_Services()
        {
            // Arrange
            var services = new ServiceCollection();
            var webHostEnvironmentMock = new Mock<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();
            services.AddSingleton(webHostEnvironmentMock.Object);
            services.AddLogging();
            services.AddControllers();
            var apiVersionDescriptionProviderMock = new Mock<IApiVersionDescriptionProvider>();
            apiVersionDescriptionProviderMock.Setup(p => p.ApiVersionDescriptions).Returns(new List<ApiVersionDescription>());
            services.AddSingleton(apiVersionDescriptionProviderMock.Object);


            // Act
            services.AddSwaggerConfigured();
            var provider = services.BuildServiceProvider();

            // Assert
            provider.GetService<ISwaggerProvider>().Should().NotBeNull();
        }

        [Fact]
        public void AddCorsConfigured_Should_Register_Cors_Services()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();
            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddCorsConfigured(configuration);
            var provider = services.BuildServiceProvider();

            // Assert
            provider.GetService<ICorsService>().Should().NotBeNull();
        }
    }
}