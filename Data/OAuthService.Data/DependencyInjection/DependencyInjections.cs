using Microsoft.Extensions.DependencyInjection;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Data.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddOAuthStorages(this IServiceCollection services)
        {
            services.AddScoped<ITokenStorage, TokenStorage>();
            services.AddScoped<IClientStorage, ClientStorage>();
            services.AddScoped<ICodeStorage, CodeStorage>();
            return services;
        }
    }
}
