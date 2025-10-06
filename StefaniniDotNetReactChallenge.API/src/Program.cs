using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

using StefaniniDotNetReactChallenge.Extensions;
using StefaniniDotNetReactChallenge.Infrastructure.Persistence;
using StefaniniDotNetReactChallenge.Infrastructure.Data;
using StefaniniDotNetReactChallenge.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

builder.Services.AddApiVersioningConfigured();
builder.Services.AddSwaggerConfigured();
builder.Services.AddCorsConfigured(builder.Configuration);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddDatabaseConfiguration(builder.Configuration)
    .AddRepositoriesConfiguration(builder.Configuration)
    .AddServicesConfiguration(builder.Configuration)
    .AddAuthenticationConfiguration(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbContextInitializer.SeedAsync(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowReactApp");
    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"),
               appBuilder =>
               {
                   appBuilder.UseRouting();
                   appBuilder.UseEndpoints(endpoints => endpoints.MapReverseProxy());
               });
}
else
{
    app.UseHttpsRedirection();

    var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(staticFilesPath)
    });
    app.MapFallbackToFile("index.html");

}

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfigured(provider);

app.UseAuthentication();

/*
*   Esse warning está sendo suprimido porquê estamos usando o UseRouting e UseEndpoints 
*   unicamente para redirecionar conexões da rota raiz diferente de /api
*   para o servidor vive, apenas em Desenvolvimento
*   by Jailton Cruz
*/
#pragma warning disable ASP0001 // Authorization middleware is incorrectly configured
app.UseAuthorization();
#pragma warning restore ASP0001 // Authorization middleware is incorrectly configured

app.MapControllers();
app.MapHealthChecks("/api/health");

app.Run();

public partial class Program { }