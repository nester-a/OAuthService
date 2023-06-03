using static OAuthConstans.ErrorResponseErrorCode;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>Выбрасывается когда не корректны код авторизации или токен обновления или пароль клиента</summary>
    public class InvalidGrantException : OAuthException
    {
        public InvalidGrantException(string? errorDescription)
            : base(InvalidGrant, errorDescription) { }
    }
}
