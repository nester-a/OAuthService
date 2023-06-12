using OAuth.Types.Abstraction;

namespace OAuthService.Types
{
    public class RevocationRequest : IRevocationRequest
    {
        public string Token { get; set; } = string.Empty;

        public string? TokenTypeHint { get; set; }
    }
}
