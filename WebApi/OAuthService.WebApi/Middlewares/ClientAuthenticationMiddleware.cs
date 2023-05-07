using Newtonsoft.Json;
using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using System.Net;

namespace OAuthService.MVC
{
    public class ClientAuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public ClientAuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var form = context.Request.Form;
            var grant = form[RequestFormField.GrantType];

            try
            {
                switch (grant)
                {
                    case GrantType.AuthorizationCode:

                    case GrantType.ResourceOwnerPasswordCredentials:
                    case GrantType.ClientCredentials:
                    case GrantType.RefreshToken:
                        break;
                    default:
                        throw new UnsupportedGrantTypeException("111");
                }

                await next.Invoke(context);
            }
            catch (OAuthException ex)
            {
                var error = new ErrorResponse(ex);
                var json = JsonConvert.SerializeObject(error);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(json);
            }
        }
    }
}