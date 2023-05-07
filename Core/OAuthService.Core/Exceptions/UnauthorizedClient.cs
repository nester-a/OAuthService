using OAuthService.Core.Constans;
using OAuthService.Core.Exceptions.Base;

namespace OAuthService.Core.Exceptions
{
    /// <summary>
    /// Выбрасывается когда клиент не может работать с этим грант типом
    /// </summary>
    public class UnauthorizedClient : OAuthException
    {
        public UnauthorizedClient(string? errorDescription)
            : base(OAuthError.UnautorizedClient, errorDescription) { }
    }
}
