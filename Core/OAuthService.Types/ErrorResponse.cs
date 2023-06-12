using OAuthConstans;
using OAuth.Types.Abstraction;
using Newtonsoft.Json;

namespace OAuthService.Types
{
    public class ErrorResponse : IErrorResponse
    {
        [JsonProperty(ErrorResponseParameter.Error)]
        public string Error { get; set; } = string.Empty;

        [JsonProperty(ErrorResponseParameter.ErrorDescription)]
        public string? ErrorDescription { get; set; }

        [JsonProperty(ErrorResponseParameter.ErrorUri)]
        public string? ErrorUri { get; set; }

        [JsonProperty(ErrorResponseParameter.State)]
        public string? State { get; set; }
    }
}
