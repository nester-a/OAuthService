using OAuthService.Core.Types;

namespace OAuthService.Interfaces.Authorization
{
    public interface IClientAuthorizationService
    {
        Task CheckClientIsAuthorizedAsync(Client client, string grantType, CancellationToken cancellationToken = default);
    }
}
