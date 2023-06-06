using OAuthConstans;
using System.Text.Json.Serialization;

namespace OAuthService.Core.Types.Responses
{
    public class AccessTokenResponse
    {
        [JsonPropertyName(AccessTokenResponseParameter.AccessToken)]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName(AccessTokenResponseParameter.TokenType)]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName(AccessTokenResponseParameter.ExpiresIn)]
        public uint ExpiresIn { get; set; }

        [JsonPropertyName(AccessTokenResponseParameter.RefreshToken)]
        public string? RefreshToken { get; set; }
    }
}
