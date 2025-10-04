using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace StefaniniDotNetReactChallenge.Tests.API.Extensions
{
    public class ApplicationBuilderExtensionsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApplicationBuilderExtensionsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/swagger/index.html")]
        [InlineData("/api/swagger/v1.0/swagger.json")]
        [InlineData("/api/swagger/v2.0/swagger.json")]
        public async Task UseSwaggerConfigured_Should_Expose_Swagger_Endpoints(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}