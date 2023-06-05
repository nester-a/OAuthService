using OAuthService.Core.Types.Responses;
using OAuthService.Exceptions.Base;
using OAuthService.Interfaces.Builders.Base;

namespace OAuthService.Interfaces.Builders
{
    public interface IErrorResponseBuilder : IBaseBuilder<ErrorResponse>
    {
        IErrorResponseBuilder AddErrorCode(string errorCode);

        IErrorResponseBuilder AddErrorDescription(string errorDescription);

        IErrorResponseBuilder AddErrorUri(string errorUri);

        IErrorResponseBuilder AddState(string state);

        IErrorResponseBuilder FromException(OAuthErrorException oAuthException);
    }
}
