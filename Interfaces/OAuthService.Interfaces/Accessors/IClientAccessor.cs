using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Accessors
{
    public interface IClientAccessor
    {
        Client Client { get; }
    }
}
