using OAuthService.Core.Enums;

namespace OAuthService.Core.Base
{
    public interface IRequest
    {
        Grant GrantType { get; }
    }
}
