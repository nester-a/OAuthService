using Microsoft.AspNetCore.Http;
using OAuthService.Middleware.Options;

namespace OAuthService.Middleware
{
    public class UserAuthorizationPageMiddleware
    {
        private readonly RequestDelegate next;
        private readonly UserAuthorizationPageOptions opt;

        public UserAuthorizationPageMiddleware(RequestDelegate next, UserAuthorizationPageOptions opt)
        {
            this.next = next;
            this.opt = opt;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == opt.uriPath)
            {
                await context.Response.SendFileAsync(opt.pageFilePath, context.RequestAborted);
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
