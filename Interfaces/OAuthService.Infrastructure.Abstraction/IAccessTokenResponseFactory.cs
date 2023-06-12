using OAuth.Types.Abstraction;
using OAuthService.Core.Entities;

namespace OAuthService.Infrastructure.Abstraction
{
    public interface IAccessTokenResponseFactory
    {
        Task<IAccessTokenResponse> CreateAsync(Client client, IAccessTokenRequest request, CancellationToken cancellation = default);
    }
}
