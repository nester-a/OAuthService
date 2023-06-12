using OAuth.Types.Abstraction;

namespace OAuthService.Infrastructure.Abstraction
{
    public interface IAuthorizationResponseFactory
    {
        Task<IAuthorizationResponse> CreateAsync(IAuthorizationRequest request, string username, string password, CancellationToken cancellation = default);
    }
}
