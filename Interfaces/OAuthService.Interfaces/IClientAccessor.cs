using OAuthService.Core.Types;

namespace OAuthService.Interfaces
{
    public interface IClientAccessor
    {
        Client Client { get; }
    }
}
