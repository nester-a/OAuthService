using OAuthService.Core.Types;
using OAuthService.Interfaces.Accessors;

namespace OAuthService.Services
{
    public class ClientAccessor : IClientAccessor
    {
        public Client Client => new Client() { Id = "123", Secret = "123", TokenKey = Guid.NewGuid().ToString() };
    }
}