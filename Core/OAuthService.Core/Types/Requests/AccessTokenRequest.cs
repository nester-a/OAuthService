using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Base;

namespace OAuthService.Core.Types.Requests
{
    public record AccessTokenRequest : ICodeGrantTokenRequest, IPasswordGrantTokenRequest, IRefreshingAccessTokenRequest
    {
        [FromForm(Name = "grant_type")]
        public string GrantType { get; } = string.Empty;

        [FromForm(Name = "code")]
        public string? Code { get; }

        [FromForm(Name = "redirect_uri")]
        public string? RedirectUri { get; }

        [FromForm(Name = "client_id")]
        public string? ClientId { get; }

        [FromForm(Name = "username")]
        public string? Username { get; }

        [FromForm(Name = "password")]
        public string? Password { get; }

        [FromForm(Name = "scope")]
        public string? Scope { get; }

        [FromForm(Name = "refresh_token")]
        public string? RefreshToken { get; }
    }
}
