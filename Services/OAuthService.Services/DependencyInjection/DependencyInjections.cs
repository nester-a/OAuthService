using Microsoft.Extensions.DependencyInjection;
using OAuthService.Core.Base;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Builders;
using OAuthService.Interfaces.Facroies;
using OAuthService.Interfaces.Processors;
using OAuthService.Interfaces.Validation;
using OAuthService.Services.Builders;
using OAuthService.Services.Factories;
using OAuthService.Services.Processors;
using OAuthService.Services.Validation;

namespace OAuthService.Services.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddOAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestProcessor<ICodeGrantTokenRequest>, CodeRequestProcessor>();
            services.AddScoped<IRequestProcessor<IPasswordGrantTokenRequest>, PasswordRequestProcessor>();
            services.AddScoped<IRequestProcessor<IClientCredentialTokenRequest>, ClientCredentialRequestProcessor>();
            services.AddScoped<IRequestProcessor<IRefreshingAccessTokenRequest>, RefreshingAccessTokenRequestProcessor>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<IAccessTokenResponseBuilder, AccessTokenResponseBuilder>();
            services.AddScoped<IPropsValidationService, PropsValidationService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IClientAccessor, ClientAccessor>();
            services.AddScoped<IResponseFactory, ResponseFactory>();
            return services;
        }
    }
}
