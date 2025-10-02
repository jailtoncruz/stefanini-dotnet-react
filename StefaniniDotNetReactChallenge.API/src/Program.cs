using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using StefaniniDotNetReactChallenge.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

builder.Services.AddApiVersioningConfigured();
builder.Services.AddSwaggerConfigured();
builder.Services.AddCorsConfigured(builder.Configuration);
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseAuthorization();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfigured(provider);

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowReactApp");
    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"), appBuilder =>
   {
       app.MapReverseProxy();
   });
}
else
{
    var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(staticFilesPath)
    });
    app.MapFallbackToFile("index.html");
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/api/health");

app.Run();