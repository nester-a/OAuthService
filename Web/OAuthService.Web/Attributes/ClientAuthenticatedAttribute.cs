using Microsoft.AspNetCore.Mvc;
using OAuthService.Web.Filters;

namespace OAuthService.Web.Attributes
{
    public class ClientAuthenticatedAttribute : TypeFilterAttribute
    {
        public ClientAuthenticatedAttribute()
            : base(typeof(ClientAuthenticationFilter)) { }
    }
}
