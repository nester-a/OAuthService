using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>Выбрасывается когда проваливается аутентификация клиента</summary>
    public class InvalidClientException : OAuthException
    {
        public InvalidClientException(string? errorDescription)
            : base(OAuthError.InvalidClient, errorDescription) { }
    }
}
