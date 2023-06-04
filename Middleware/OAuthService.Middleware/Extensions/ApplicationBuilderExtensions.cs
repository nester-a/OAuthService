using Microsoft.AspNetCore.Builder;
using OAuthService.Middleware.Options;

namespace OAuthService.Middleware.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOAuthService(this IApplicationBuilder app, UserAuthorizationPageOptions userAuthorizationPageOptions)
        {
            app.UseMiddleware<OAuthErrorHandleMiddleware>();
            app.UseMiddleware<UserAuthorizationPageMiddleware>(userAuthorizationPageOptions);
            app.UseMiddleware<ClientAuthenticationMiddleware>();
            app.UseMiddleware<ClientAuthorizationMiddleware>();
            app.UseMiddleware<GrantTypeValidationMiddleware>();

            return app;
        }
    }
}
