using OAuthService.Core.Base;
using OAuthConstans;
using OAuthService.Core.Exceptions.Base;
using System.Text.Json.Serialization;

namespace OAuthService.Core.Types.Responses
{
    public class ErrorResponse : IResponse
    {
        [JsonPropertyName(ErrorResponseParameter.Error)]
        public string Error { get; set; } = string.Empty;

        [JsonPropertyName(ErrorResponseParameter.ErrorDescription)]
        public string? ErrorDescription { get; set; }

        [JsonPropertyName(ErrorResponseParameter.ErrorUri)]
        public string? ErrorUri { get; set; }

        [JsonPropertyName(ErrorResponseParameter.State)]
        public string? State { get; set; }


        public ErrorResponse(OAuthException exception)
        {
            Error = exception.Error;
            ErrorDescription = exception.ErrorDescription;
        }

        public ErrorResponse() { }
    }
}
