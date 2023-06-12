using OAuthService.Core.Entities;

namespace OAuthService.Data.Abstraction
{
    public interface IClientStorage
    {
        Task<Client> GetClientByIdAndNullableSecretAsync(string id, string? secret = null, CancellationToken cancellationToken = default);
    }
}
