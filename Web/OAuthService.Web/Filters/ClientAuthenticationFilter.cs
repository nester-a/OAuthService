using Microsoft.AspNetCore.Mvc.Filters;
using OAuthService.Core.Enums;
using OAuthService.Core.Types;
using OAuthService.Exceptions;

namespace OAuthService.Web.Filters
{
    public class ClientAuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Items[ItemKey.Client] is not Client)
            {
                throw new InvalidClientException("Client is not authenticated");
            }
        }
    }
}
