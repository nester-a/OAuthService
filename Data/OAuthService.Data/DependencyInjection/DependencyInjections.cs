using Microsoft.Extensions.DependencyInjection;
using OAuthService.Data.Abstraction;

namespace OAuthService.Data.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddTestOAuthStorages(this IServiceCollection services)
        {
            services.AddScoped<ITokenStorage, TokenStorage>();
            services.AddScoped<IClientStorage, ClientStorage>();
            services.AddScoped<ICodeStorage, CodeStorage>();
            services.AddScoped<IUserStorage, UserStorage>();
            return services;
        }
    }
}
