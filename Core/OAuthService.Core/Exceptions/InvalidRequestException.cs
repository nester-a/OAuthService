using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>Выбрасывается когда в запросе есть лишние параметры или не хватает нужных</summary>
    public class InvalidRequestException : OAuthException
    {
        public InvalidRequestException(string? errorDescription)
            : base(OAuthError.InvalidRequest, errorDescription) { }
    }
}
