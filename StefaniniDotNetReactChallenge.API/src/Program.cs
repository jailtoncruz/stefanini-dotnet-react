using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

using StefaniniDotNetReactChallenge.Extensions;
using StefaniniDotNetReactChallenge.Configurations;
using StefaniniDotNetReactChallenge.Infrastructure.Persistence;
using StefaniniDotNetReactChallenge.Infrastructure.Data;

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
    .AddServicesConfiguration(builder.Configuration);


var app = builder.Build();

app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbContextInitializer.SeedAsync(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowReactApp");
}
else
{
    app.UseHttpsRedirection();

    var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(staticFilesPath)
    });
}
app.UseAuthorization();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfigured(provider);

app.MapControllers();
app.MapHealthChecks("/api/health");

if (app.Environment.IsDevelopment())
{
    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"),
                appBuilder => { app.MapReverseProxy(); });
}
else
{
    app.MapFallbackToFile("index.html");
}

app.Run();