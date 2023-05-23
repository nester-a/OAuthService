using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Accessors
{
    public interface IClientAccessor
    {
        Client? Client { get; }

        Task<bool> TrySetClientByIdAndSecretAsync(string clientId, string clientSecret, CancellationToken cancellation = default);
        Task<bool> TrySetClientByIdAsync(string clientSecret, CancellationToken cancellation = default);
    }
}
