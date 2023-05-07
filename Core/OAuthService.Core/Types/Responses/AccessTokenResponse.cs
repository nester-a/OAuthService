using OAuthService.Core.Base;
using System.Text.Json.Serialization;

namespace OAuthService.Core.Types.Responses
{
    public class AccessTokenResponse : IResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public uint ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonIgnore]
        public string State => "success";
    }
}
