using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Storages
{
    public interface IClientStorage
    {
        Task<Client> GetClientByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}
