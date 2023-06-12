using OAuthService.Core.Entities;

namespace OAuthService.Infrastructure.Abstraction
{
    public interface IClientAuthorizationService
    {
        Task CheckClientIsAuthorizedAsync(Client client, string grantType, CancellationToken cancellationToken = default);
    }
}
