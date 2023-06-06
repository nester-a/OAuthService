using OAuthService.Core.Base;
using OAuthService.Core.Types;
using OAuthService.Core.Types.Requests;
using OAuthService.Core.Types.Responses;

namespace OAuthService.Interfaces.Facroies
{
    public interface IRequestResponseFactory
    {
        Task<AccessTokenResponse> CreateResponseAsync(Client responseAud, AccessTokenRequest request, CancellationToken cancellationToken = default);
    }
}
