using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Storages
{
    public interface IClientStorage
    {
        Task<Client> GetClientByIdAndNullableSecretAsync(string id, string? secret = null, CancellationToken cancellationToken = default);
    }
}
