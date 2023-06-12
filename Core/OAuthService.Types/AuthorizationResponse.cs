using OAuth.Types.Abstraction;
using OAuthConstans;
using System.Text.Json.Serialization;

namespace OAuthService.Types
{
    public class AuthorizationResponse : IAuthorizationResponse
    {
        [JsonPropertyName(AuthorizationResponseParameter.Code)]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName(AuthorizationResponseParameter.State)]
        public string State { get; set; } = string.Empty;
    }
}
