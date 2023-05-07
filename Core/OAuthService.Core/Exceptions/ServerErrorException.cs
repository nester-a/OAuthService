using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    public class ServerErrorException : OAuthException
    {
        public ServerErrorException(string? errorDescription)
            : base(OAuthError.ServerError, errorDescription) { }
    }
}
