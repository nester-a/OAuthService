using OAuthConstans;
using System.Text.Json.Serialization;
using OAuthService.Exceptions.Base;

namespace OAuthService.Core.Types.Responses
{
    public class ErrorResponse
    {
        [JsonPropertyName(ErrorResponseParameter.Error)]
        public string Error { get; set; } = string.Empty;

        [JsonPropertyName(ErrorResponseParameter.ErrorDescription)]
        public string? ErrorDescription { get; set; }

        [JsonPropertyName(ErrorResponseParameter.ErrorUri)]
        public string? ErrorUri { get; set; }

        [JsonPropertyName(ErrorResponseParameter.State)]
        public string? State { get; set; }


        public ErrorResponse(OAuthErrorException exception)
        {
            Error = exception.ErrorCode;
            ErrorDescription = exception.ErrorDescription;
        }

        public ErrorResponse() { }
    }
}
