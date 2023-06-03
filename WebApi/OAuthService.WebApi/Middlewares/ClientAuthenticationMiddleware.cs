using Newtonsoft.Json;
using OAuthConstans;
using static OAuthConstans.AccessTokenRequestParameters;
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
            else
            {
                var form = context.Request.Form;
                var grant = form[GrantType].ToString();

                switch (grant)
                {
                    case AccessTokenRequestGrantType.AuthorizationCode when string.IsNullOrWhiteSpace(form[ClientId]):
                    case AccessTokenRequestGrantType.Password:
                    case AccessTokenRequestGrantType.ClientCredentials:
                    case AccessTokenRequestGrantType.RefreshToken:
                        await clientAuthenticationService.AuthenticateClientByAuthorizationHeaderAsync(context.Request.Headers);
                        break;
                    case AccessTokenRequestGrantType.AuthorizationCode:
                        await clientAuthenticationService.AuthenticateClientByIdAsync(form[ClientId]!);
                        break;
                    default:
                        throw new UnsupportedGrantTypeException("Request grant type is not supported by this service");
                }

                await next.Invoke(context);
            }
        }
    }
}