using OAuth.Types.Abstraction;
using OAuthService.Exceptions.Base;
using OAuthService.Infrastructure.Types;

namespace OAuthService.Infrastructure.Builders
{
    public class ErrorResponseBuilder
    {
        readonly ErrorResponse response = new();

        public ErrorResponseBuilder AddErrorCode(string errorCode)
        {
            response.Error = errorCode;
            return this;
        }

        public ErrorResponseBuilder AddErrorDescription(string errorDescription)
        {
            response.ErrorDescription = errorDescription;
            return this;
        }

        public ErrorResponseBuilder AddErrorUri(string errorUri)
        {
            response.ErrorUri = errorUri;
            return this;
        }

        public ErrorResponseBuilder AddState(string state)
        {
            response.State = state;
            return this;
        }

        public ErrorResponseBuilder FromException(OAuthErrorException oAuthException)
        {
            AddErrorCode(oAuthException.ErrorCode);
            return AddErrorDescription(oAuthException.ErrorDescription);
        }

        public IErrorResponse Build() => response;
    }
}
