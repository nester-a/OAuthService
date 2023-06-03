using static OAuthConstans.ErrorResponseErrorCode;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    public class ServerErrorException : OAuthException
    {
        public ServerErrorException(string? errorDescription)
            : base(ServerError, errorDescription) { }
    }
}
