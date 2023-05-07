using Microsoft.AspNetCore.Mvc;

namespace OAuthService.Core.Types.Requests
{
    public class AuthorizationRequest
    {
        [FromQuery(Name = "response_type")]
        public string ResponseType { get; set; } = string.Empty;

        [FromQuery(Name = "client_id")]
        public string ClientId { get; set; } = string.Empty;

        [FromQuery(Name = "redirect_uri")]
        public string? RedirectUri { get; set; }

        [FromQuery(Name = "scope")]
        public string? Scope { get; set; }

        [FromQuery(Name = "state")]
        public string? State { get; set; }
    }
}
