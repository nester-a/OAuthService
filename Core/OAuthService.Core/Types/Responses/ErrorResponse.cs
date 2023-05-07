using OAuthService.Core.Base;
using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions.Base;
using System.Text.Json.Serialization;

namespace OAuthService.Core.Types.Responses
{
    public class ErrorResponse : IResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; } = string.Empty;

        [JsonPropertyName("error_description")]
        public string? ErrorDescription { get; set; }

        [JsonPropertyName("error_uri")]
        public string? ErrorUri { get; set; }

        [JsonPropertyName("state")]
        public string State => "error";


        public ErrorResponse(OAuthException exception)
        {
            Error = exception.Error;
            ErrorDescription = exception.ErrorDescription;
        }
    }
}
