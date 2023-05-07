using OAuthService.Core.Types;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Data
{
    public class ClientStorage : IClientStorage
    {
        public async Task<Client> GetClientByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new Client() { Id = "123", Secret = "123", TokenKey = "123" };
        }
    }
}
