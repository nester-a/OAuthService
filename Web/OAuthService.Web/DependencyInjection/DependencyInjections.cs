using OAuthService.Core.Base;
using OAuthService.Data;
using OAuthService.Interfaces;
using OAuthService.Interfaces.Authorization;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Storages;
using OAuthService.Interfaces.Validation;
using OAuthService.Services;
using OAuthService.Services.Authorization;
using OAuthService.Services.Builders;
using OAuthService.Services.Factories;
using OAuthService.Services.Processors;
using OAuthService.Services.Validation;

namespace OAuthService.Web.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
        {
            services.AddScoped<IErrorResponseBuilder, ErrorResponseBuilder>();
            services.AddScoped<IResponsePreparationService, ResponsePreparationService>();
            return services;
        }

        public static IServiceCollection AddTokenEndpointServices(this IServiceCollection services)
        {
            services.AddScoped<IClientAuthorizationService, ClientAuthorizationService>();

            services.AddScoped<IPropsValidationService, PropsValidationService>();
            services.AddScoped<IValidationService, ValidationService>();

            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<IAccessTokenResponseBuilder, AccessTokenResponseBuilder>();
            services.AddScoped<IRequestProcessor<ICodeGrantTokenRequest>, CodeRequestProcessor>();

            services.AddScoped<IRequestProcessor<IPasswordGrantTokenRequest>, PasswordRequestProcessor>();
            services.AddScoped<IRequestProcessor<IClientCredentialTokenRequest>, ClientCredentialRequestProcessor>();
            services.AddScoped<IRequestProcessor<IRefreshingAccessTokenRequest>, RefreshingAccessTokenRequestProcessor>();

            services.AddScoped<IResponseFactory, ResponseFactory>();

            return services;
        }


        public static IServiceCollection AddStorages(this IServiceCollection services)
        {
            services.AddScoped<IClientStorage, ClientStorage>();
            services.AddScoped<ICodeStorage, CodeStorage>();
            services.AddScoped<ITokenStorage, TokenStorage>();
            services.AddScoped<IUserStorage, UserStorage>();
            return services;
        }
    }
}
