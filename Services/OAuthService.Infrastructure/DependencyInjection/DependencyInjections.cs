using Microsoft.Extensions.DependencyInjection;
using OAuthService.Infrastructure.Abstraction;
using OAuthService.Infrastructure.Factories;
using OAuthService.Infrastructure.Services;
using OAuthService.Interfaces.Validation;

namespace OAuthService.Infrastructure.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddOAuthServiceInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IResponsePreparationService, ResponsePreparationService>();
            services.AddScoped<IClientAuthorizationService, ClientAuthorizationService>();
            services.AddScoped<IAccessTokenRequestValidationService, AccessTokenRequestValidationService>();

            services.AddScoped<IAccessTokenResponseFactory, AccessTokenResponseFactory>();
            services.AddScoped<AccessTokenResponseBuilderFactory>();
            services.AddScoped<ErrorResponseBuilderFactory>();
            services.AddScoped<JwtBuilderFactory>();

            services.AddScoped<IAuthorizationResponseFactory, AuthorizationResponseFactory>();
            services.AddScoped<AuthorizationResponseBuilderFactory>();
            return services;
        }
    }
}
