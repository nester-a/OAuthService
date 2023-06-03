using static OAuthConstans.ErrorResponseErrorCode;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>
    /// Выбрасывается когда клиент не может работать с этим грант типом
    /// </summary>
    public class UnauthorizedClientException : OAuthException
    {
        public UnauthorizedClientException(string? errorDescription)
            : base(UnauthorizedClient, errorDescription) { }
    }
}
