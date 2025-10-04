using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Moq;
using StefaniniDotNetReactChallenge.API.Configurations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace StefaniniDotNetReactChallenge.Tests.API.Configuration
{
    public class ConfigureSwaggerOptionsTests
    {
        [Fact]
        public void Configure_Should_Create_Swagger_Docs_For_All_Api_Versions()
        {
            // Arrange
            var apiVersionDescriptionProviderMock = new Mock<IApiVersionDescriptionProvider>();
            var apiVersionDescriptions = new[]
            {
                new ApiVersionDescription(new(1, 0), "v1", false),
                new ApiVersionDescription(new(2, 0), "v2", false)
            };
            apiVersionDescriptionProviderMock.Setup(p => p.ApiVersionDescriptions).Returns(apiVersionDescriptions);

            var options = new SwaggerGenOptions();
            var configureSwaggerOptions = new ConfigureSwaggerOptions(apiVersionDescriptionProviderMock.Object);

            // Act
            configureSwaggerOptions.Configure(options);

            // Assert
            options.SwaggerGeneratorOptions.SwaggerDocs.Should().HaveCount(2);
            options.SwaggerGeneratorOptions.SwaggerDocs.Should().ContainKey("v1");
            options.SwaggerGeneratorOptions.SwaggerDocs.Should().ContainKey("v2");

            var v1Doc = options.SwaggerGeneratorOptions.SwaggerDocs["v1"];
            v1Doc.Title.Should().Be("Stefanini Challenge API");
            v1Doc.Version.Should().Be("1.0");

            var v2Doc = options.SwaggerGeneratorOptions.SwaggerDocs["v2"];
            v2Doc.Title.Should().Be("Stefanini Challenge API");
            v2Doc.Version.Should().Be("2.0");
        }
    }
}
