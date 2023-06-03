using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders;

namespace OAuthService.Services.Builders
{
    public class ErrorResponseBuilder : IErrorResponseBuilder
    {
        ErrorResponse response = new();
        public IErrorResponseBuilder AddErrorCode(string errorCode)
        {
            response.Error = errorCode;
            return this;
        }

        public IErrorResponseBuilder AddErrorDescription(string errorDescription)
        {
            response.ErrorDescription = errorDescription;
            return this;
        }

        public IErrorResponseBuilder AddErrorUri(string errorUri)
        {
            response.ErrorUri = errorUri;
            return this;
        }

        public IErrorResponseBuilder AddState(string state)
        {
            response.State = state;
            return this;
        }

        public IErrorResponseBuilder FromException(OAuthException oAuthException)
        {
            response = new(oAuthException);
            return this;
        }

        public ErrorResponse Build() => response;
    }
}
