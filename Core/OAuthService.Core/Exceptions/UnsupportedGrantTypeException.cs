using static OAuthConstans.ErrorResponseErrorCode;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>Выбрасывается когда сервис не поддерживает грант тип пришедший в запросе</summary>
    public class UnsupportedGrantTypeException : OAuthException
    {
        public UnsupportedGrantTypeException(string? errorDescription)
            : base(UnsupportedGrantType, errorDescription) { }
    }
}
