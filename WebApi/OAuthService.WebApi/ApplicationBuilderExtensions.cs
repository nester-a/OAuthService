using OAuthService.Middleware;
using OAuthService.Middleware.Options;
using OAuthService.MVC;

namespace OAuthService.WebApi
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOAuthService(this IApplicationBuilder app, UserAuthorizationPageOptions userAuthorizationPageOptions)
        {
            app.UseMiddleware<OAuthErrorHandleMiddleware>();
            app.UseMiddleware<UserAuthorizationPageMiddleware>(userAuthorizationPageOptions);
            app.UseMiddleware<ClientAuthenticationMiddleware>();

            return app;
        }
    }
}
