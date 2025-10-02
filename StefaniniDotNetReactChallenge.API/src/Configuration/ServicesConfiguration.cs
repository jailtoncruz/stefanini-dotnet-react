
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Application.Services;

namespace StefaniniDotNetReactChallenge.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}
