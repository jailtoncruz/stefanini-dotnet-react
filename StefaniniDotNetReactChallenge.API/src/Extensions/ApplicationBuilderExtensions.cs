using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace StefaniniDotNetReactChallenge.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerConfigured(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api/swagger";
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/api/swagger/{description.GroupName}/swagger.json",
                        $"MyApi {description.GroupName.ToUpperInvariant()}");
                }
            });

            return app;
        }
    }
}
