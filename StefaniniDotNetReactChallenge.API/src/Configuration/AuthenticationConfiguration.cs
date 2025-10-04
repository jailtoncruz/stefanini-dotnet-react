using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StefaniniDotNetReactChallenge.API.Configurations;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"]!;
        var issuer = configuration["Jwt:Issuer"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });

        services.AddAuthorization();

        return services;
    }
}