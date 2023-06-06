using Microsoft.AspNetCore.Mvc;
using OAuthConstans;

namespace OAuthService.Core.Types.Requests
{
    public record AuthorizationRequest
    {
        [FromQuery(Name = AuthorizationRequestParameter.ResponseType)]
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
