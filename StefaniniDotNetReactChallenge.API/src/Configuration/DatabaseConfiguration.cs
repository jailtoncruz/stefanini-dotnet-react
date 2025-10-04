using Microsoft.EntityFrameworkCore;
using StefaniniDotNetReactChallenge.Infrastructure.Data;

namespace StefaniniDotNetReactChallenge.API.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("StefaniniDB"));
        return services;
    }
}
