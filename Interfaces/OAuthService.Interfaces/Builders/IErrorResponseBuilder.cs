using OAuthService.Core.Base;
using OAuthService.Core.Exceptions.Base;
using OAuthService.Core.Types.Responses;
using OAuthService.Interfaces.Builders.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService.Interfaces.Builders
{
    public interface IErrorResponseBuilder : IBaseBuilder<ErrorResponse>
    {
        IErrorResponseBuilder AddErrorCode(string errorCode);

        IErrorResponseBuilder AddErrorDescription(string errorDescription);

        IErrorResponseBuilder AddErrorUri(string errorUri);

        IErrorResponseBuilder AddState(string state);

        IErrorResponseBuilder FromException(OAuthException oAuthException);
    }
}
