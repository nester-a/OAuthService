using OAuth.Types.Abstraction;
using System.Text.Json.Serialization;

namespace OAuthService.Types
{
    public class AuthorizationResponse : IAuthorizationResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
    }
}
