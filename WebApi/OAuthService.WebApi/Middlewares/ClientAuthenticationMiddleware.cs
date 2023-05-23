using Newtonsoft.Json;
using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces;
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
        public async Task InvokeAsync(HttpContext context, IClientAuthenticationService clientAuthenticationService)
        {
            var path = context.Request.Path;

            if(path.HasValue && !path.Value.Contains("/Token"))
            {
                await next.Invoke(context);
            }

            var form = context.Request.Form;
            var grant = form[RequestFormField.GrantType].ToString();

            try
            {
                switch (grant)
                {
                    case GrantType.AuthorizationCode when string.IsNullOrWhiteSpace(form[RequestFormField.ClientId]):
                    case GrantType.ResourceOwnerPasswordCredentials:
                    case GrantType.ClientCredentials:
                    case GrantType.RefreshToken:
                        await clientAuthenticationService.AuthenticateClientByAuthorizationHeaderAsync(context.Request.Headers);
                        break;
                    case GrantType.AuthorizationCode:
                        await clientAuthenticationService.AuthenticateClientByIdAsync(form[RequestFormField.ClientId]!);
                        break;
                    default:
                        throw new UnsupportedGrantTypeException("Request grant type is not supported by this service");
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