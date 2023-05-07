using OAuthService.Core.Base;
using OAuthService.Core.Types.Requests;

namespace OAuthService.Interfaces
{
    public interface IRequestFactory
    {
        Task<IRequest> CreateRequestAsync(AccessTokenRequest request, CancellationToken cancellation = default);
    }
}
