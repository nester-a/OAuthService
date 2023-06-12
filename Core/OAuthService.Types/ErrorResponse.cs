using OAuthConstans;
using System.Text.Json.Serialization;
using OAuth.Types.Abstraction;

namespace OAuthService.Types
{
    public class ErrorResponse : IErrorResponse
    {
        [JsonPropertyName(ErrorResponseParameter.Error)]
        public string Error { get; set; } = string.Empty;

        [JsonPropertyName(ErrorResponseParameter.ErrorDescription)]
        public string? ErrorDescription { get; set; }

        [JsonPropertyName(ErrorResponseParameter.ErrorUri)]
        public string? ErrorUri { get; set; }

        [JsonPropertyName(ErrorResponseParameter.State)]
        public string? State { get; set; }
    }
}
