using Microsoft.AspNetCore.Http;
using OAuthConstans;
using OAuthService.Core.Enums;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Types;

namespace OAuthService.Middleware
{
    public class ClientAuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        public ClientAuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var client = context.Items[ItemKey.Client] as Client;

            if (client is null)
            {
                throw new ServerErrorException("Client not setted");
            }

            var form = context.Request.Form;

            var allowed = client.SupportedGrantTypes.Contains(form[AccessTokenRequestParameter.GrantType].ToString());

            if(!allowed)
            {
                throw new UnauthorizedClientException("The authenticated client is not authorized to use this authorization grant type.");
            }

            await next.Invoke(context);
        }
    }
}
