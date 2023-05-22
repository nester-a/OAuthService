﻿using Microsoft.AspNetCore.Mvc;
using OAuthService.Core.Base;

namespace OAuthService.Core.Types.Requests
{
    public record AccessTokenRequest : ICodeGrantTokenRequest, IPasswordGrantTokenRequest, IRefreshingAccessTokenRequest
    {
        [FromForm(Name = "grant_type")]
        public string GrantType { get; set; } = string.Empty;

        [FromForm(Name = "code")]
        public string? Code { get; set; }

        [FromForm(Name = "redirect_uri")]
        public string? RedirectUri { get; set; }

        [FromForm(Name = "client_id")]
        public string? ClientId { get; set;  }

        [FromForm(Name = "username")]
        public string? Username { get; set;  }

        [FromForm(Name = "password")]
        public string? Password { get; set; }

        [FromForm(Name = "scope")]
        public string? Scope { get; set; }

        [FromForm(Name = "refresh_token")]
        public string? RefreshToken { get; set; }
    }
}