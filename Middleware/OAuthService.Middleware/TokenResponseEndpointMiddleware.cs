using Microsoft.AspNetCore.Http;
using OAuthConstans;

namespace OAuthService.Middleware
{
    public class TokenResponseEndpointMiddleware
    {
        private readonly RequestDelegate next;

        public TokenResponseEndpointMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Path == ProtocolEndpoint.Token)
            {

            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
