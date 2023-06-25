using Microsoft.AspNetCore.Mvc;
using OAuth.Types.Abstraction;
using OAuthConstans;
using OAuthService.Web.Attributes;

namespace OAuthService.Web.Models
{
    public record AuthorizationRequest : IAuthorizationRequest
    {
        [FromQuery(Name = AuthorizationRequestParameter.ResponseType)]
        [AllowedStringValues(AuthorizationRequestResponseType.Token, AuthorizationRequestResponseType.Code)]
        public string ResponseType { get; set; } = string.Empty;

        [FromQuery(Name = AuthorizationRequestParameter.ClientId)]
        public string ClientId { get; set; } = string.Empty;

        [FromQuery(Name = AuthorizationRequestParameter.RedirectUri)]
        public string? RedirectUri { get; set; }

        [FromQuery(Name = AuthorizationRequestParameter.Scope)]
        public string? Scope { get; set; }

        [FromQuery(Name = AuthorizationRequestParameter.State)]
        public string? State { get; set; }
    }
}
