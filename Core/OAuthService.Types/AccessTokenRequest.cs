﻿using Microsoft.AspNetCore.Mvc;
using OAuth.Types.Abstraction;
using OAuthConstans;

namespace OAuthService.Types
{
    public record AccessTokenRequest : IAccessTokenRequest
    {
        [FromForm(Name = AccessTokenRequestParameter.GrantType)]
        public string GrantType { get; set; } = string.Empty;

        [FromForm(Name = AccessTokenRequestParameter.Code)]
        public string? Code { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.RedirectUri)]
        public string? RedirectUri { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.ClientId)]
        public string? ClientId { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.ClientSecret)]
        public string? ClientSecret { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.Username)]
        public string? Username { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.Password)]
        public string? Password { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.Scope)]
        public string? Scope { get; set; }

        [FromForm(Name = AccessTokenRequestParameter.RefreshToken)]
        public string? RefreshToken { get; set; }
    }
}