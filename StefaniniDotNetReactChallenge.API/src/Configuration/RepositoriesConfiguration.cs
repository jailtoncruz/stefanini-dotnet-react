using StefaniniDotNetReactChallenge.Domain.Interfaces;
using StefaniniDotNetReactChallenge.Infrastructure.Repositories;

namespace StefaniniDotNetReactChallenge.Configurations;

public static class RepositoriesConfiguration
{
    public static IServiceCollection AddRepositoriesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}
