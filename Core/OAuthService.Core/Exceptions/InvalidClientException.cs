using static OAuthConstans.ErrorResponseErrorCode;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>Выбрасывается когда проваливается аутентификация клиента</summary>
    public class InvalidClientException : OAuthException
    {
        public InvalidClientException(string? errorDescription)
            : base(InvalidClient, errorDescription) { }
    }
}
