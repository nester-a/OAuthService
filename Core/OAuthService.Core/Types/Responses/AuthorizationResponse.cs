using System.Text.Json.Serialization;

namespace OAuthService.Core.Types.Responses
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
    }
}
