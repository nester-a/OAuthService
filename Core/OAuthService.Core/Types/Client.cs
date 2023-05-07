﻿namespace OAuthService.Core.Types
{
    public class Client
    {
        public string Id { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string TokenKey { get; set; } = string.Empty;
        public string[] SupportedGrantTypes { get; set; } = Array.Empty<string>();
        public bool RequiresRefreshToken { get; set; }
    }
}
