using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests;

namespace OAuthService.Interfaces.Facroies
{
    public interface IResponseFactory
    {
        Task<IResponse> CreateResponseAsync(AccessTokenRequest request, CancellationToken cancellationToken = default);
    }
}
