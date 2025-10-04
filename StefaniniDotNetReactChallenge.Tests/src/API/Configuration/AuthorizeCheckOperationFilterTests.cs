using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Moq;
using Swashbuckle.AspNetCore.SwaggerGen;
using StefaniniDotNetReactChallenge.API.Configurations;
using Xunit;

namespace StefaniniDotNetReactChallenge.Tests.Configurations
{
    public class AuthorizeCheckOperationFilterTests
    {
        private readonly AuthorizeCheckOperationFilter _filter = new();

        [Fact]
        public void Apply_Should_Add_Security_When_HasAuthorize_Without_AllowAnonymous()
        {
            // Arrange
            var methodInfo = typeof(SecuredController).GetMethod(nameof(SecuredController.ProtectedMethod))!;
            var operation = new OpenApiOperation();
            var context = CreateOperationFilterContext(methodInfo);

            // Act
            _filter.Apply(operation, context);

            // Assert
            Assert.NotNull(operation.Security);
            Assert.Single(operation.Security);
            var securityScheme = operation.Security.First().Keys.First();
            Assert.Equal("Bearer", securityScheme.Reference.Id);
        }

        [Fact]
        public void Apply_Should_Not_Add_Security_When_HasAllowAnonymous()
        {
            // Arrange
            var methodInfo = typeof(SecuredController).GetMethod(nameof(SecuredController.AnonymousMethod))!;
            var operation = new OpenApiOperation();
            var context = CreateOperationFilterContext(methodInfo);

            // Act
            _filter.Apply(operation, context);

            // Assert
            Assert.Empty(operation.Security);
        }

        [Fact]
        public void Apply_Should_Not_Add_Security_When_NoAuthorize()
        {
            // Arrange
            var methodInfo = typeof(OpenController).GetMethod(nameof(OpenController.OpenMethod))!;
            var operation = new OpenApiOperation();
            var context = CreateOperationFilterContext(methodInfo);

            // Act
            _filter.Apply(operation, context);

            // Assert
            Assert.Empty(operation.Security);
        }

        private static OperationFilterContext CreateOperationFilterContext(MethodInfo methodInfo)
        {
            var apiDesc = new Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription();
            var schemaRepo = new SchemaRepository();
            var schemaGen = new Mock<ISchemaGenerator>().Object;
            return new OperationFilterContext(apiDesc, schemaGen, schemaRepo, methodInfo);
        }

        // Helper test controllers
        [Authorize]
        private class SecuredController
        {
            public void ProtectedMethod() { }

            [AllowAnonymous]
            public void AnonymousMethod() { }
        }

        private class OpenController
        {
            public void OpenMethod() { }
        }
    }
}
