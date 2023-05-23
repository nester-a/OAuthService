using OAuthService.Core.Types;
using OAuthService.Interfaces.Accessors;
using OAuthService.Interfaces.Storages;

namespace OAuthService.Services
{
    public class ClientAccessor : IClientAccessor
    {
        private readonly IClientStorage clientStorage;

        public ClientAccessor(IClientStorage clientStorage)
        {
            this.clientStorage = clientStorage;
        }

        public Client? Client { get; private set; }

        public async Task<bool> TrySetClientByIdAndSecretAsync(string clientId, string clientSecret, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            Client = await clientStorage.GetClientByIdAndNullableSecretAsync(clientId, clientSecret, cancellation);

            if (Client is null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> TrySetClientByIdAsync(string clientId, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            Client = await clientStorage.GetClientByIdAndNullableSecretAsync(clientId, null, cancellation);

            if (Client is null)
            {
                return false;
            }

            return true;
        }
    }
}