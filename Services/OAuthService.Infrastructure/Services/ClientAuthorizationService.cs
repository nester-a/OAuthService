using OAuthService.Core.Entities;
using OAuthService.Exceptions;
using OAuthService.Infrastructure.Abstraction;

namespace OAuthService.Infrastructure.Services
{
    public class ClientAuthorizationService : IClientAuthorizationService
    {
        public async Task CheckClientIsAuthorizedAsync(Client client, string grantType, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Factory.StartNew(() =>
            {
                var authorized = client.SupportedGrantTypes.Contains(grantType);
                if (!authorized)
                {
                    throw new UnauthorizedClientException(nameof(grantType));
                }
            }, cancellationToken);
        }
    }
}
